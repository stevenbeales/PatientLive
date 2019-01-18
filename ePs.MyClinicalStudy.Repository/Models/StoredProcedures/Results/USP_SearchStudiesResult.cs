using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results
{
    public class USP_SearchStudiesResult
    {
        // Stored Procedure Result 
        public Int64 RN { get; set; }
        public Nullable<int> StudyId { get; set; }
        public Nullable<int> SiteId { get; set; }
        public string NCTID { get; set; }
        public string Title { get; set; }
        public string Sponsor { get; set; }
        public string StudyStatus { get; set; }
        public string EligibilityCriteriaTextblock { get; set; }
        public string EligibilityGender { get; set; }
        public Nullable<byte> EligibilityMinAgeYears { get; set; }
        public Nullable<byte> EligibilityMaxAgeYears { get; set; }
        public string EligibilityHealthyVolunteersYN { get; set; }
        public string Phase { get; set; }
        public string StudyType { get; set; }
        public Nullable<int> Enrollment { get; set; }
        public Nullable<System.DateTime> StudyStartDate { get; set; }
        public Nullable<System.DateTime> StudyEndDate { get; set; }
        public string Conditions { get; set; }
        public Nullable<int> SurveyId { get; set; }
        public Nullable<int> Distance { get; set; }
        public string StudyPrimaryContact { get; set; }
        public string StudyBackupContact { get; set; }
        public string SitePrimaryContact { get; set; }
        public string SiteBackupContact { get; set; }
        public string City { get; set; }
        public Nullable<int> IsNew { get; set; }
        public Nullable<int> TotalItems { get; set; }
    }
}