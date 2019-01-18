using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class Medication
    {
        public Medication()
        {
            this.MedicationConditions = new List<MedicationCondition>();
            this.UserMedications = new List<UserMedication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<MedicationCondition> MedicationConditions { get; set; }
        public virtual ICollection<UserMedication> UserMedications { get; set; }
    }
}
