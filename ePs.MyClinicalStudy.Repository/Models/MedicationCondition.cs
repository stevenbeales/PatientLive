using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class MedicationCondition
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public int ConditionId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual Medication Medication { get; set; }
    }
}
