using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Views
{
    public class vw_SavedStudy
    {
        // View Properties
        public int UserStudyId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Conditions { get; set; }
        public string EligibilityCriteriaTextblock { get; set; }
        public string EligibilityGender { get; set; }
        public string EligibilityHealthyVolunteersYN { get; set; }
        public byte EligibilityMaxAgeYears { get; set; }
        public byte EligibilityMinAgeYears { get; set; }
        public Nullable<int> Enrollment { get; set; }
        public string Phase { get; set; }
        public string SiteBackupContact { get; set; }
        public string SitePrimaryContact { get; set; }
        public string Sponsor { get; set; }
        public string StudyBackupContact { get; set; }
        public Nullable<System.DateTime> StudyEndDate { get; set; }
        public string StudyStatus { get; set; }
        public string StudyType { get; set; }
        public Nullable<int> SurveyId { get; set; }
        public int StudyId { get; set; }
        public int SiteId { get; set; }
        public string City { get; set; }
    }
}