using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class UserSearch
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Conditions { get; set; }
        public string Medications { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> Distance { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual User User { get; set; }
    }
}
