using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class Study
    {
        public Study()
        {
            this.StudyConditions = new List<StudyCondition>();
            this.StudySites = new List<StudySite>();
            this.UserStudies = new List<UserStudy>();
        }

        public int Id { get; set; }
        public string NCTID { get; set; }
        public string Title { get; set; }
        public string Sponsor { get; set; }
        public string Status { get; set; }
        public string EligibilityCriteriaTextblock { get; set; }
        public string EligibilityGender { get; set; }
        public string EligibilityMinAge { get; set; }
        public string EligibilityMaxAge { get; set; }
        public byte EligibilityMinAgeYears { get; set; }
        public byte EligibilityMaxAgeYears { get; set; }
        public string EligibilityHealthyVolunteersYN { get; set; }
        public string Phase { get; set; }
        public string StudyType { get; set; }
        public Nullable<int> Enrollment { get; set; }
        public Nullable<System.DateTime> StudyStartDate { get; set; }
        public Nullable<System.DateTime> StudyEndDate { get; set; }
        public string VisitSchedule { get; set; }
        public string PrimaryCondition { get; set; }
        public Nullable<int> SurveyId { get; set; }
        public string Source { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<StudyCondition> StudyConditions { get; set; }
        public virtual ICollection<StudySite> StudySites { get; set; }
        public virtual ICollection<UserStudy> UserStudies { get; set; }
    }
}
