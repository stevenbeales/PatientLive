using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.ObjectModel;

using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.ViewModels;

namespace ePs.PatientLive.Framework.DataModel
{
    public static class DataService
    {
        #region :: Private Members ----------------------------------------------------------------

        private static readonly string ConnectionString = "https://prod11.epharmasolutions.com/MyClinicalStudyService/v2/MyClinicalStudy.svc";
        private static readonly MyClinicalStudyContext Context = new MyClinicalStudyContext(new Uri(ConnectionString));
        private static MyClinicalStudyContext StudyContext;
        private static readonly string MyClinicalStudyPath = "http://prod11.epharmasolutions.com/survey/TakeSurvey.aspx?SurveyID=";

        #endregion

        #region :: Properties ---------------------------------------------------------------------

        public static IEnumerable<USP_SearchStudiesResult> StudyResults { get; set; }
        public static IEnumerable<USP_GetNewItemCountsResult> ItemCountResults { get; set; }

        public static Gradient StudyTileBg = new Gradient
        {
            Start = HexToColorConverter.ConvertFromHex("#FF7FA3CF"),
            End = HexToColorConverter.ConvertFromHex("#FF3E80CF")
        };

        public static Gradient NewTileBg = new Gradient
        {
            Start = HexToColorConverter.ConvertFromHex("#FF57d763"),
            End = HexToColorConverter.ConvertFromHex("#FF3ea747")
        };

        #endregion

        #region :: Events/Event Handlers ----------------------------------------------------------

        public delegate void CollectionChangedHandler(EventArgs e);
        private static CollectionChangedHandler _collectionChanged;
        public static event CollectionChangedHandler CollectionChanged
        {
            add
            {
                if (_collectionChanged == null)
                {
                    _collectionChanged += value;
                }
                else
                {
                    foreach (Delegate del in _collectionChanged.GetInvocationList())
                    {
                        _collectionChanged -= (CollectionChangedHandler)del;
                    }

                    _collectionChanged += value;
                }
            }
            remove
            {
                _collectionChanged -= value;
            }
        }

        #endregion

        #region :: User Methods -------------------------------------------------------------------

        public static async Task<User> GetUser(string liveId)
        {
            IEnumerable<User> user =
                await ((DataServiceQuery<User>)Context.Users.Expand("UserConditions/Condition,UserMedications/Medication,UserStudies/Study").Where(u => u.WindowsLiveID.Equals(liveId)).Select(o => o)).ExecuteAsync();
            
            return user.FirstOrDefault();
        }

        public static User CreateUser(string liveId, string firstName, string lastName, string email)
        {
            var user = new User
            {
                username      = email,
                WindowsLiveID = liveId,
                active_yn     = "y",
                first_name    = firstName,
                last_name     = lastName,
                email_address = email,
                Created       = DateTime.Now,
                CreatedBy     = email,
                Updated       = DateTime.Now,
                UpdatedBy     = email
            };

            Context.AddObject("Users", user);
            Context.BeginSaveChanges(null, null);
            return user;
        }

        public static void CreateUserAccessLog(User user, string activity, string tileName)
        {
            var log = new UserAccessLog
            {
                User      = user,
                User_Id   = user.user_id,
                Activity  = activity,
                TileName  = tileName,
                Created   = DateTime.Now,
                CreatedBy = user.username
            };

            Context.AddObject("UserAccessLogs", log);
            Context.BeginSaveChanges(null, null);
        }

        public static void CreateUserSearchLog(User user, int? age, string gender, string conditions, string medications, string postalCode, int? distance)
        {
            var log = new UserSearch
            {
                 User        = user,
                 User_Id     = user.user_id,
                 Age         = age,
                 Conditions  = conditions,
                 Medications = medications,
                 PostalCode  = postalCode,
                 Distance    = distance,
                 Created     = DateTime.Now,
                 CreatedBy   = user.username,
                 Gender      = gender == "\"\"" ? string.Empty : gender               
            };

            Context.AddObject("UserSearches", log);
            Context.BeginSaveChanges(null, null);
        }

        public static async Task SaveUser(User user, string conditions, string medications)
        {      
            //var userConditions = new DataServiceCollection<UserCondition>(Context);
            if (conditions.Length > 0)
            {
                var condArray = conditions.Split(';');
                var condList = SuspensionManager.SessionState["UserConditions"] as List<string>;
                foreach (var con in condArray)
                {
                    if (con.Trim().Length > 0)
                    {
                        var c = await GetConditionByName(con.Trim());
                        if (c != null)
                        {
                            var userCondition = new UserCondition
                            {
                                User_Id     = user.user_id,
                                ConditionId = c.Id
                            };
                            if (user.UserConditions.Where(o => o.ConditionId == userCondition.ConditionId).FirstOrDefault() == null)
                            {
                                Context.AddObject("UserConditions", userCondition);
                            }

                            if(!condList.Contains(c.Name))
                                condList.Add(c.Name);
                        }
                    }
                }
                SuspensionManager.SessionState["UserConditions"] = condList;
                var conditionsToRemove = user.UserConditions.Where(o => !condArray.Contains(o.Condition.Name));
                foreach (var condition in conditionsToRemove)
                {
                    user.UserConditions.Remove(condition);
                }
            }
            else 
            {
                user.UserConditions.Clear();
                SuspensionManager.SessionState["UserConditions"] = null;
            }

            //var userMeds = new DataServiceCollection<UserMedication>();
            if (medications.Length > 0)
            {
                var medArray = medications.Split(';');
                var medList = SuspensionManager.SessionState["UserMedications"] as List<string>;
                foreach (var med in medArray)
                {
                    if (med.Length > 0)
                    {
                        var m = await GetMedicationByName(med);
                        if (m != null)
                        {
                            var userMedication = new UserMedication
                            {
                                User_Id      = user.user_id,
                                MedicationId = m.Id
                            };
                            if (user.UserMedications.Where(o => o.MedicationId == userMedication.MedicationId).FirstOrDefault() == null)
                            { 
                                Context.AddObject("UserMedications", userMedication);                               
                            }

                            if(!medList.Contains(m.Name))
                                medList.Add(m.Name);
                        }
                    }
                }

                SuspensionManager.SessionState["UserMedications"] = medList;

                var medsToRemove = user.UserMedications.Where(o => !medArray.Contains(o.Medication.Name));
                foreach (var med in medsToRemove)
                {
                    user.UserMedications.Remove(med);
                }
            }
            else
            {
                user.UserMedications.Clear();
                SuspensionManager.SessionState["UserMedications"] = null;
            }

            user.Updated   = DateTime.Now;
            user.UpdatedBy = user.username;

            Context.UpdateObject(user);
            Context.BeginSaveChanges(null, null);          
        }

        public static async Task SaveUser(User user, List<Condition> conditions, List<Medication> medications)
        {
            var conditionsToDelete = user.UserConditions.Where(o => o.Deleted != null);
            foreach (var con in conditionsToDelete)
            {
                Context.UpdateObject(con);
            }
            
            if (conditions.Count > 0)
            {
                foreach (var con in conditions)
                {
                    var userCondition = user.UserConditions.Where(o => o.ConditionId == con.Id).FirstOrDefault();
                    if (userCondition == null)
                    {
                        userCondition = new UserCondition
                        {
                            User_Id     = user.user_id,
                            ConditionId = con.Id,
                            Created     = DateTime.Now,
                            CreatedBy   = user.username,
                            Updated     = DateTime.Now,
                            UpdatedBy   = user.username
                        };
                        Context.AddObject("UserConditions", userCondition);
                    }
                    else
                    {
                        userCondition.Deleted   = null;
                        userCondition.DeletedBy = null;
                        Context.UpdateObject(userCondition);
                    }              
                }
            }     

            var medsToDelete = user.UserMedications.Where(o => o.Deleted != null);
            foreach (var med in medsToDelete)
            {
                Context.UpdateObject(med);
            }
            
            if (medications.Count > 0)
            {
                foreach (var med in medications)
                {
                    var userMed = user.UserMedications.Where(o => o.MedicationId == med.Id).FirstOrDefault();

                    if (userMed == null)
                    {
                        userMed = new UserMedication
                        {
                            User_Id      = user.user_id,
                            MedicationId = med.Id,
                            Created      = DateTime.Now,
                            CreatedBy    = user.username,
                            Updated      = DateTime.Now,
                            UpdatedBy    = user.username
                        };
                        Context.AddObject("UserMedications", userMed);
                    }
                    else
                    {
                        userMed.Deleted   = null;
                        userMed.DeletedBy = null;
                        Context.UpdateObject(userMed);
                    }                   
                }
            }

            user.Updated   = DateTime.Now;
            user.UpdatedBy = user.username;

            Context.UpdateObject(user);
            Context.BeginSaveChanges(null, null);
        }

        #endregion

        #region :: List Helper Methods ------------------------------------------------------------

        #region Safety Alerts

        public static async Task<ServiceResult<SafetyTile>> GetSafetyAlerts(int userId, uint skip, uint take)
        {
            var alerts = await ((DataServiceQuery<vw_GetAlert>)Context.vw_GetAlerts).ExecuteAsync(); //.Where(o => o.UserId.Equals(userId))

            var items = (from a in alerts
                         select new SafetyTile
                         {
                             Title     = a.Title,
                             Info      = a.Description,
                             URL       = a.URL,
                             AlertDate = a.AlertDate,
                             Subtitle  = string.Empty
                         }).ToList<SafetyTile>();

            return new ServiceResult<SafetyTile>
            {
                Items = items.Skip((int)skip).Take((int)take),
                TotalItemsCount = Convert.ToUInt32(items.Count)
            };
        }

        public static async Task<IEnumerable<SafetyTile>> GetSafetyAlerts(int userId)
        {
            var alerts = await ((DataServiceQuery<vw_GetAlert>)Context.vw_GetAlerts.Where(o => o.UserId.Equals(userId))).ExecuteAsync(); //.Where(o => o.UserId.Equals(userId))

            var items = (from a in alerts
                         select new SafetyTile
                         {
                             Title     = a.Title,
                             Info      = a.Description,
                             URL       = a.URL,
                             AlertDate = a.AlertDate,
                             Subtitle  = string.Empty,
                             Background = a.IsNew == 0 ? StudyTileBg : NewTileBg
                         });

            return items;
        }

        #endregion

        #region Study Tiles

        public static async Task<IEnumerable<StudyTile>> GetMyStudyTiles(int userId)
        {
            var myStudies = await ((DataServiceQuery<vw_SavedStudy>)Context.vw_SavedStudies.Where(o => o.UserId.Equals(userId))).ExecuteAsync();

            var studyTiles = (from c in myStudies
                                select new StudyTile
                                {
                                    Title               = c.Title,
                                    Condition           = c.Conditions,
                                    Eligibility         = c.EligibilityCriteriaTextblock,
                                    Gender              = c.EligibilityGender,
                                    HealthyVolunteersYN = c.EligibilityHealthyVolunteersYN,
                                    MaxAge              = c.EligibilityMaxAgeYears,
                                    MinAge              = c.EligibilityMinAgeYears,
                                    Enrollment          = c.Enrollment,
                                    Phase               = c.Phase,
                                    SiteBackupContact   = c.SiteBackupContact,
                                    SitePrimaryContact  = c.SitePrimaryContact,
                                    Sponsor             = c.Sponsor,
                                    StudyBackupContact  = c.StudyBackupContact,
                                    StudyEndDate        = c.StudyEndDate,
                                    Status              = c.StudyStatus,
                                    StudyType           = c.StudyType,
                                    SurveyId            = c.SurveyId,
                                    StudyId             = c.StudyId,
                                    City                = c.City,
                                    URL                 = c.SurveyId != null && c.SurveyId != 0 ? MyClinicalStudyPath + c.SurveyId.ToString() : string.Empty
                                }).ToList();

            return studyTiles;
        }
        
        public static async Task<ServiceResult<StudyTile>> GetStudyTiles(string dob, string gender, string conditions, string postalCode, string country, Nullable<int> distance, Nullable<decimal> lat, Nullable<decimal> lon, uint skip, uint take, int userId)
        {
            var condArray = conditions.Split(';');
            var studies   = await ((DataServiceQuery<SearchStudy>)Context.SearchStudies.Where(o => o.Conditions.Contains(condArray[0]))).ExecuteAsync();

            var studyTiles = (from c in studies
                              select new StudyTile
                              {
                                  Title               = c.Title,
                                  Condition           = c.Conditions,
                                  Eligibility         = c.EligibilityCriteriaTextblock,
                                  Gender              = c.EligibilityGender,
                                  HealthyVolunteersYN = c.EligibilityHealthyVolunteersYN,
                                  MaxAge              = c.EligibilityMaxAgeYears,
                                  MinAge              = c.EligibilityMinAgeYears,
                                  Enrollment          = c.Enrollment,
                                  Phase               = c.Phase,
                                  SiteBackupContact   = c.SiteBackupContact,
                                  SitePrimaryContact  = c.SitePrimaryContact,
                                  Sponsor             = c.Sponsor,
                                  StudyBackupContact  = c.StudyBackupContact,
                                  StudyEndDate        = c.StudyEndDate,
                                  Status              = c.StudyStatus,
                                  StudyType           = c.StudyType,
                                  SurveyId            = c.SurveyId,
                                  StudyId             = c.StudyId,
                                  URL                 = c.SurveyId != null && c.SurveyId != 0 ? MyClinicalStudyPath + c.SurveyId.ToString() : string.Empty
                              }).ToList();

            return new ServiceResult<StudyTile>
            {
                Items           = studyTiles.Skip(Convert.ToInt32(skip)).Take(Convert.ToInt32(take)),
                TotalItemsCount = Convert.ToUInt32(studyTiles.Count())
            };
        }

        #endregion

        #region Research Studies

        public static void GetResearchStudies(int? age, string gender, string conditions, string postalCode, string country,
                                                             int? distance, decimal? lat, decimal? lon, uint skip, uint take, int userId, string dob)
        {
            string queryString = string.Format("GetStudies?age={0}&gender='{1}'&conditions='{2}'&postalCode='{3}'&country='{4}'&distance={5}&latitude={6}m&longitude={7}m&skip={8}&take={9}&userid={10}&dob='{11}'",
                                                age, gender, conditions, postalCode, country, distance, lat, lon, skip, take, userId, dob);
            
            StudyContext = new MyClinicalStudyContext(new Uri(ConnectionString));
            StudyContext.BeginExecute<USP_SearchStudiesResult>(new Uri(queryString, UriKind.Relative), StudiesResultCallback, null);
        }

        private static void StudiesResultCallback(IAsyncResult asyncResult)
        {
            StudyResults = StudyContext.EndExecute<USP_SearchStudiesResult>(asyncResult);
            
            if (_collectionChanged != null)
            {
                _collectionChanged(new EventArgs());
            }
        }

        #endregion

        #region New Item Counts

        public static void GetNewItemCount(int userId)
        {
            var queryString = string.Format("GetNewItemCounts?userId={0}", userId);
            Context.BeginExecute<USP_GetNewItemCountsResult>(new Uri(queryString, UriKind.Relative), NewItemCountResultCallback, null);
        }

        private static void NewItemCountResultCallback(IAsyncResult asyncResult)
        {
            ItemCountResults = Context.EndExecute<USP_GetNewItemCountsResult>(asyncResult);

            if (_collectionChanged != null)
            {
                _collectionChanged(new EventArgs());
            }
        }

        #endregion

        public static async Task<IEnumerable<string>> GetConditions(string conditionName)
        {
            IEnumerable<Condition> conditions =
                await ((DataServiceQuery<Condition>)Context.Conditions.Where(c => c.Name.StartsWith(conditionName)).Select(o => o)).ExecuteAsync();

            return conditions.Select(o => o.Name);
        }

        public static async Task<IEnumerable<string>> GetMedications(string medicationName)
        {
            var medications =
                await ((DataServiceQuery<Medication>)Context.Medications.Where(c => c.Name.StartsWith(medicationName)).Select(o => o)).ExecuteAsync();

            return medications.Select(o => o.Name);
        }

        public static async Task<IEnumerable<string>> GetMappedConditions(string conditionName)
        {
            IEnumerable<MedInfoCondition> conditions =
                await ((DataServiceQuery<MedInfoCondition>)Context.MedInfoConditions.Where(c => c.Name.StartsWith(conditionName)).Select(o => o)).ExecuteAsync();

            return conditions.Select(o => o.Name);
        }

        public static async Task<IEnumerable<string>> GetMappedMedications(string medicationName)
        {
            IEnumerable<MedInfoDrug> meds =
                await ((DataServiceQuery<MedInfoDrug>)Context.MedInfoDrugs.Where(c => c.Name.StartsWith(medicationName)).Select(o => o)).ExecuteAsync();

            return meds.Select(o => o.Name);
        }

        public static async Task<IEnumerable<string>> GetCountries()
        {
            IEnumerable<Country> countries =
                await ((DataServiceQuery<Country>)Context.Countries.Select(o => o)).ExecuteAsync();

            return countries.Select(o => o.Name);
        }

        public static async Task<IEnumerable<string>> GetStates()
        {
            IEnumerable<StateProvince> states =
                await ((DataServiceQuery<StateProvince>)Context.StateProvinces.Select(o => o)).ExecuteAsync();

            return states.Select(o => o.Abbreviation);
        }

        public static async Task<ServiceResult<EducationTile>> GetEducationFAQ(uint skip, uint take)
        {
            var faq = await ((DataServiceQuery<EducationalMaterialsFAQ>)Context.EducationalMaterialsFAQs.OrderBy(o => o.Id)).ExecuteAsync();

            var items = (from f in faq
                         select new EducationTile
                         {
                             Info     = f.Answer,
                             Answer   = f.Answer,
                             Question = f.Question,
                             Subtitle = f.Question,
                             Title    = "Educational Material",
                             Id       = f.Id
                         }).ToList<EducationTile>().OrderBy(o => o.Id);

            return new ServiceResult<EducationTile>
            {
                TotalItemsCount = Convert.ToUInt32(items.Count()),
                Items           = items.Skip((int)skip).Take((int)take)
            };
        }

        public static async Task<Condition> GetConditionByName(string conditionName)
        {
            var condition = await ((DataServiceQuery<Condition>)Context.Conditions.Where(o => o.Name.Equals(conditionName))).ExecuteAsync();
            return condition.FirstOrDefault();
        }

        public static async Task<Medication> GetMedicationByName(string medicationName)
        {
            var medication = await ((DataServiceQuery<Medication>)Context.Medications.Where(o => o.Name.Equals(medicationName))).ExecuteAsync();
            return medication.FirstOrDefault();
        }

        public static async Task<ServiceResult<MedTile>> GetMedTiles(string conditions, string medications, uint skip, uint take)
        {
            var con = conditions.Split(';').Where(o => o != string.Empty);
            var med = medications.Split(';').Where(o => o != string.Empty);

            var meds = await ((DataServiceQuery<vw_SearchMedication>)Context.vw_SearchMedications).ExecuteAsync();

            var medTiles = (from m in meds
                            where con.Contains(m.ConditionName) || med.Contains(m.DrugName)
                            select new MedTile
                            {
                                TradeName     = m.DrugName ?? string.Empty,
                                Info          = m.Information ?? string.Empty,
                                Title         = m.DrugName ?? string.Empty,
                                Subtitle      = string.Empty,
                                Background    = m.IsNew == 0 ? StudyTileBg : NewTileBg,
                                GenericName   = m.ActiveIngredient ?? string.Empty,
                                Manufacturer  = m.Sponsors ?? string.Empty
                            }).ToList();

            var total = medTiles.Count();

            return new ServiceResult<MedTile>
            {
                Items           = medTiles.Skip(Convert.ToInt32(skip)).Take(Convert.ToInt32(take)),
                TotalItemsCount = Convert.ToUInt32(total),
                GetMore = false
            };
        }

        #endregion

        #region :: Study Methods ------------------------------------------------------------------

        public static async void SaveStudies(User user, List<string> studyIds)
        {
            var currentStudies = user.UserStudies.Where(o => o.Deleted == null).Select(o => o.StudyId);
            foreach (var id in studyIds)
            {
                var ids = id.Split(',');
                if (!currentStudies.Contains(Int32.Parse(ids[0])))
                {
                    SaveStudy(user, Int32.Parse(ids[0]), Int32.Parse(ids[1]));
                }
            }
        }

        public static void SaveStudy(User user, int studyId, int siteId)
        {
            var study = user.UserStudies.Where(o => o.StudyId == studyId).FirstOrDefault();
            if (study == null)
            {
                study = new UserStudy
                {
                    User = user,
                    User_Id = user.user_id,
                    StudyId = studyId,
                    SiteId = siteId,
                    Created = DateTime.Now,
                    CreatedBy = user.username
                };
                user.UserStudies.Add(study);
                Context.AddObject("UserStudies", study);
            }
            else 
            {
                study.Deleted = null;
                study.DeletedBy = null;
                Context.UpdateObject(study);
            }

            SuspensionManager.SessionState["CurrentUser"] = user;
            Context.BeginSaveChanges(null, null);
        }

        public static void RemoveSavedStudies(User user, List<int> studyIds)
        {
            foreach (var id in studyIds)
            {
                RemoveSavedStudy(user, id);
            }
        }
        
        public static async void RemoveSavedStudy(User user, int studyId)
        {
            IEnumerable<UserStudy> studyResult = await ((DataServiceQuery<UserStudy>)Context.UserStudies.Where(o => o.StudyId.Equals(studyId) && o.User_Id.Equals(user.user_id))).ExecuteAsync();
            var study = studyResult.FirstOrDefault();
            if (study != null)
            {
                study.Deleted   = DateTime.Now;
                study.DeletedBy = user.username;
                Context.UpdateObject(study);
                Context.BeginSaveChanges(null, null);
                SuspensionManager.SessionState["CurrentUser"] = user;
                //var studyToRemove = user.UserStudies.Where(o => o.StudyId == study.StudyId && o.Deleted == null).FirstOrDefault();
                //if (studyToRemove != null)
                //{
                //    user.UserStudies.Remove(studyToRemove);
                //    user.UserStudies.Add(study);
                    
                //}
            }
        }

        public static async Task<StudyTile> GetStudyById(int studyId)
        {
            IEnumerable<Study> study =
                await ((DataServiceQuery<Study>)Context.Studies.Where(o => o.Id.Equals(studyId)).Select(o => o)).ExecuteAsync();

            var studyTile = (from s in study
                            select new StudyTile
                            {
                                Title       = s.Title, 
                                Condition   = s.PrimaryCondition,
                                Eligibility = s.EligibilityCriteriaTextblock
                            }).FirstOrDefault();

            return studyTile;
        }

        #endregion
    }
}