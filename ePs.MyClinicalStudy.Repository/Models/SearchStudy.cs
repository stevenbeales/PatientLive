using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class SearchStudy
    {
        // Properties
        public int StudyId { get; set; }
        public string NCTID { get; set; }
        public string Title { get; set; }
        public string Sponsor { get; set; }
        public string StudyStatus { get; set; }
        public string EligibilityCriteriaTextblock { get; set; }
        public string EligibilityGender { get; set; }
        public byte EligibilityMinAgeYears { get; set; }
        public byte EligibilityMaxAgeYears { get; set; }
        public string EligibilityHealthyVolunteersYN { get; set; }
        public string Phase { get; set; }
        public string StudyType { get; set; }
        public Nullable<int> Enrollment { get; set; }
        public Nullable<System.DateTime> StudyStartDate { get; set; }
        public Nullable<System.DateTime> StudyEndDate { get; set; }
        public string Conditions { get; set; }
        public Nullable<int> SurveyId { get; set; }
        public string StudyPrimaryContact { get; set; }
        public string StudyBackupContact { get; set; }
        public string SiteName { get; set; }
        public string PILastName { get; set; }
        public string PIFirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string SiteStatus { get; set; }
        public string SitePrimaryContact { get; set; }
        public string SiteBackupContact { get; set; }
    }
}