//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using ePs.MyClinicalStudy.Repository.Models;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.Web.MyClinicalStudy.Service
{
    public class MyClinicalStudy : DataService<MyClinicalStudyContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            config.UseVerboseErrors = true;

            // Entity Sets
            config.SetEntitySetAccessRule("Conditions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("EducationalMaterialsFAQs", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Medications", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("MedicationConditions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Sites", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Studies", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("StudyConditions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("StudySites", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Users", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("UserAccessLogs", EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("UserConditions", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("UserMedications", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("UserSearches", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("UserStudies", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Countries", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("StateProvinces", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("MedInfoConditions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("MedInfoDrugs", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("MedInfoDrugConditions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("SearchStudies", EntitySetRights.AllRead);

            // View Sets
            config.SetEntitySetAccessRule("vw_GetAlerts", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("vw_SearchMedications", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("vw_SavedStudies", EntitySetRights.AllRead);

            //// Stored Procedure Result Sets
            config.SetEntitySetAccessRule("USP_SearchMedicationsResults", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("USP_GetNewItemCountsResults", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("USP_GetAlertsResults", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("USP_SearchStudiesResults", EntitySetRights.AllRead);

            // Service Operation Sets
            config.SetServiceOperationAccessRule("GetStudies", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetMedications", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetNewItemCounts", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetAlerts", ServiceOperationRights.AllRead);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [WebGet]
        public IEnumerable<USP_SearchStudiesResult> GetStudies(
            string dob,
            int age,
            string gender,
            string conditions,
            string postalCode,
            string country,
            Nullable<int> distance,
            Nullable<decimal> latitude,
            Nullable<decimal> longitude,
            Nullable<int> skip,
            Nullable<int> take,
            Nullable<int> userId)
        {
            // Get the ObjectContext that is the data source for the service.
            MyClinicalStudyContext context = this.CurrentDataSource;

            try
            {
                var results =
                    context.Database.ExecuteStoredProcedure(new USP_SearchStudies()
                                                            {
                                                                DOB        = DateTime.Parse(dob),
                                                                Age        = age,
                                                                Gender     = gender,
                                                                Conditions = conditions,
                                                                PostalCode = postalCode,
                                                                Country    = country,
                                                                Distance   = distance,
                                                                Latitude   = latitude,
                                                                Longitude  = longitude,
                                                                Skip       = skip,
                                                                Take       = take,
                                                                user_id    = userId
                                                            });
                return results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        [WebGet]
        public IEnumerable<USP_SearchMedicationsResult> GetMedications(
            string conditions,
            string medications,
            Nullable<int> skip,
            Nullable<int> take,
            Nullable<int> userId)
        {
            MyClinicalStudyContext context = this.CurrentDataSource;

            try
            {
                var results =
                    context.Database.ExecuteStoredProcedure(new USP_SearchMedications()
                    {
                        Conditions  = conditions,
                        Medications = medications,
                        Skip        = skip,
                        Take        = take,
                        user_id     = userId
                    });

                return results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        [WebGet]
        public IEnumerable<USP_GetNewItemCountsResult> GetNewItemCounts(Nullable<int> userId)
        {
            MyClinicalStudyContext context = this.CurrentDataSource;

            try
            {
                var results =
                    context.Database.ExecuteStoredProcedure(new USP_GetNewItemCounts()
                    {
                        user_id = userId
                    });

                return results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", ex.Message));
            }
        }

        [WebGet]
        public IEnumerable<USP_GetAlertsResult> GetAlerts(Nullable<int> userId)
        {
            MyClinicalStudyContext context = this.CurrentDataSource;

            try
            {
                var results =
                    context.Database.ExecuteStoredProcedure(new USP_GetAlerts()
                    {
                        user_id = userId
                    });

                return results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", ex.Message));
            }
        }
    }
}