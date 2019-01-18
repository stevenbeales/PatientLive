using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class Site
    {
        public Site()
        {
            this.StudySites  = new List<StudySite>();
            this.UserStudies = new List<UserStudy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PILastName { get; set; }
        public string PIFirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        //public System.Data.Spatial.DbGeography LatLong { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<StudySite> StudySites { get; set; }
        public virtual ICollection<UserStudy> UserStudies { get; set; }
    }
}
