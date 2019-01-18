using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class MedInfoDrugCondition
    {
        public int Id { get; set; }
        public int MedInfoDrugId { get; set; }
        public int MedInfoConditionId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual MedInfoDrug MedInfoDrug { get; set; }
        public virtual MedInfoCondition MedInfoCondition { get; set; }
    }
}