using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures
{
    public class USP_SearchStudies : IStoredProcedure<USP_SearchStudiesResult>
    {
        // Stored Procedure Parameters
        public Nullable<DateTime> DOB { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Conditions { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Nullable<int> Distance { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<int> Skip { get; set; }
        public Nullable<int> Take { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}