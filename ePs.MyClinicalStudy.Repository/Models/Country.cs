using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class Country
    {
        public Country()
        {
            this.CountryStateProvinces = new List<StateProvince>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string ISOAlpha2 { get; set; }
        public string ISOAlpha3 { get; set; }
        public virtual ICollection<StateProvince> CountryStateProvinces { get; set; }
    }
}