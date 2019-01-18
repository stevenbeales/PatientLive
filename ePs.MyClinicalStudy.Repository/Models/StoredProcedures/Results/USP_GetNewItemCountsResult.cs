using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results
{
    public class USP_GetNewItemCountsResult
    {
        // Stored Procedure Result 
        public string Item { get; set; }
        public Nullable<int> Count { get; set; }
    }
}