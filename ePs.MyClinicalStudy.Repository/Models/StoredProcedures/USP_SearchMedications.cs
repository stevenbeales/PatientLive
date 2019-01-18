using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures
{
    public class USP_SearchMedications : IStoredProcedure<USP_SearchMedicationsResult>
    {
        // Stored Procedure Parameters
        public string Conditions { get; set; }
        public string Medications { get; set; }
        public Nullable<int> Skip { get; set; }
        public Nullable<int> Take { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}