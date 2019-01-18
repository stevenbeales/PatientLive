using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class Condition
    {
        public Condition()
        {
            this.MedicationConditions = new List<MedicationCondition>();
            this.StudyConditions = new List<StudyCondition>();
            this.UserConditions = new List<UserCondition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<MedicationCondition> MedicationConditions { get; set; }
        public virtual ICollection<StudyCondition> StudyConditions { get; set; }
        public virtual ICollection<UserCondition> UserConditions { get; set; }
    }
}
