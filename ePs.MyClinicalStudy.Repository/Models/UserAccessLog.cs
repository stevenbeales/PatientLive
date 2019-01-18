using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class UserAccessLog
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string TileName { get; set; }
        public string Activity { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public virtual User User { get; set; }
    }
}