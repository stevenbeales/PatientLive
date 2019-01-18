using ePs.PatientLive.Framework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.Infrastructure;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class ProfileModel : INotifyPropertyChanged
    {

        private string _fullName;
        public string FullName 
        {
            get 
            {
                return _fullName;
            }
            set 
            {
                if (_fullName == value) return;
                _fullName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FullName"));
            } 
        }
        
        private User _currentUser;
        public User CurrentUser 
        {
            get 
            {
                return _currentUser;
            }
            set
            {                
                _currentUser = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        private string _genderString;
        public string GenderString
        { 
            get { return _genderString; }
            set
            {
                if (_genderString == value) return;
                _genderString = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GenderString"));
            }
        }

        private List<Condition> _conditions;
        public List<Condition> Conditions
        {
            get { return _conditions; }
            set 
            {
                _conditions = value;
                //if(PropertyChanged != null)
                //    PropertyChanged(this, new PropertyChangedEventArgs("Conditions"));
            }
        }

        private List<Medication> _medications;
        public List<Medication> Medications
        {
            get { return _medications; }
            set
            {
                _medications = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Medications"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedConditions
        {
            get;
            set;
        }

        public string SelectedMedications
        {
            get;
            set;
        }

        public string DateOfBirth
        {
            get 
            {
                string dob = string.Empty;

                if (CurrentUser.DOB.HasValue)
                {
                    dob = CurrentUser.DOB.Value.ToString("d");
                }

                return dob;
            }
        }

        public Boolean UserModified { get; set; }

        public List<int> DayList { get; set; }
        public List<int> MonthList { get; set; }
        public List<int> YearList { get; set; }
        public ObservableCollection<IEnumerable<string>> CountryList { get; set; }
        public ObservableCollection<IEnumerable<string>> StateList { get; set; }
        public string ConditionString { get; set; }
        public string MedicationString { get; set; }
        public bool SavingData { get; set; }

        public ProfileModel()
        {
            CurrentUser      = SuspensionManager.SessionState["CurrentUser"] as User;
            Medications      = SuspensionManager.SessionState["UserMedications"] as List<Medication>;
            Conditions       = SuspensionManager.SessionState["UserConditions"] as List<Condition>;
            FullName         = CurrentUser.first_name + " " + CurrentUser.last_name;
            GenderString     = !string.IsNullOrWhiteSpace(CurrentUser.Gender) ? CurrentUser.Gender == "M" ? "Male" : "Female" : string.Empty;
            ConditionString  = GetConditionString(CurrentUser);
            MedicationString = GetMedicationString(CurrentUser);
            DayList          = new List<int>();
            MonthList        = new List<int>();
            YearList         = new List<int>();
            SavingData       = false;
        }

        public async void RefreshCurrentUser(string liveId)
        {
            CurrentUser = await DataService.GetUser(liveId);
        }

        public List<string> GetUserConditions(User user)
        {
            return user.UserConditions.Select(o => o.Condition.Name).ToList();
        }

        public List<string> GetUserMedications(User user)
        {
            return user.UserMedications.Select(o => o.Medication.Name).ToList();
        }

        public async Task<IEnumerable<string>> GetResults(string searchString, string type)
        {
            switch(type)
            {
                case "conditions":
                    return await DataService.GetConditions(searchString);
                case "medications":
                    return await DataService.GetMedications(searchString);
                default: return null;
            }

        }

        public async Task SaveUser(User user, string conditions, string medications)
        {

            var condList = SuspensionManager.SessionState["UserConditions"] as List<Condition>;
            var newConditions = new List<Condition>();
            if (conditions.Length > 0)
            {
                var condArray = conditions.Split(';');
                foreach (var con in condArray)
                {
                    if (con.Trim().Length > 0)
                    {
                        var c = await DataService.GetConditionByName(con.Trim());
                        if (c != null)
                        {
                            if (!condList.Contains(c))
                            {
                                condList.Add(c);
                                newConditions.Add(c);
                            }
                        }
                    }
                }
                
                //Conditions = condList;
                foreach (var condition in user.UserConditions.Where(o => !condArray.Contains(o.Condition.Name)))
                {
                    condition.Deleted = DateTime.Now;
                    condition.DeletedBy = user.username;

                    condList.Remove(condition.Condition);
                }
                SuspensionManager.SessionState["UserConditions"] = condList;
            }
            else
            {
                foreach (var uc in user.UserConditions)
                {
                    uc.Deleted = DateTime.Now;
                    uc.DeletedBy = user.username;
                }
                
                SuspensionManager.SessionState["UserConditions"] = new List<Condition>();
                condList = new List<Condition>();
            }

            //var userMeds = new DataServiceCollection<UserMedication>();
            var medList = SuspensionManager.SessionState["UserMedications"] as List<Medication>;
            var newMeds = new List<Medication>();
            if (medications.Length > 0)
            {
                var medArray = medications.Split(';');
                
                foreach (var med in medArray)
                {
                    if (med.Length > 0)
                    {
                        var m = await DataService.GetMedicationByName(med);
                        if (m != null)
                        {
                            if (!medList.Contains(m))
                            {
                                medList.Add(m);
                                newMeds.Add(m);
                            }
                        }
                    }
                }

                foreach (var med in user.UserMedications.Where(o => !medArray.Contains(o.Medication.Name)))
                {
                    med.Deleted = DateTime.Now;
                    med.DeletedBy = user.username;

                    medList.Remove(med.Medication);
                }
                SuspensionManager.SessionState["UserMedications"] = medList;
            }
            else
            {
                foreach (var med in user.UserMedications)
                {
                    med.Deleted = DateTime.Now;
                    med.DeletedBy = user.username;
                }
                SuspensionManager.SessionState["UserMedications"] = new List<Medication>();
                medList = new List<Medication>();
            }

            await DataService.SaveUser(user, newConditions, newMeds);
            Conditions = SuspensionManager.SessionState["UserConditions"] as List<Condition>;
            Medications = SuspensionManager.SessionState["UserMedications"] as List<Medication>;
        }

        public async Task<IEnumerable<string>> GetCountryList()
        {
            var countries = await DataService.GetCountries() as IEnumerable<string>;
            return countries;
        }

        public async Task<IEnumerable<string>> GetStateList()
        {
            var states = await DataService.GetStates() as IEnumerable<string>;
            return states;
        }

        private string GetConditionString(User user)
        {
            var list = string.Empty;
            var conditions = SuspensionManager.SessionState["UserConditions"] as List<Condition>;
            foreach (Condition condition in conditions)
            {
                list += condition.Name + ";";
            }
            return list;
        }

        private string GetMedicationString(User user)
        {
            string list = string.Empty;
            List<Medication> medications = SuspensionManager.SessionState["UserMedications"] as List<Medication>;

            foreach (Medication medication in medications)
            {
                list += medication.Name + ";";
            }
            return list;
        }

    }
}
