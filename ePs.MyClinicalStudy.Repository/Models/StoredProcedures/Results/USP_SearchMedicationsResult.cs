using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results
{
    public class USP_SearchMedicationsResult
    {
        // Stored Procedure Result 
        public string DrugName { get; set; }
        public string Information { get; set; }
        public string HREF { get; set; }
        public string Conditions { get; set; }
        public Nullable<int> IsNew { get; set; }
        public Nullable<int> TotalItems { get; set; }
    }
}