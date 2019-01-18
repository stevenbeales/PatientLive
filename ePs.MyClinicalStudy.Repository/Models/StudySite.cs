using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class StudySite
    {
        public int Id { get; set; }
        public int StudyId { get; set; }
        public int SiteId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual Site Site { get; set; }
        public virtual Study Study { get; set; }
    }
}
