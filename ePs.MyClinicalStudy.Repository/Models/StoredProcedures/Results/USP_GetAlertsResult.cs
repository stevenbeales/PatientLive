using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results
{
    public class USP_GetAlertsResult
    {
        // Stored Procedure Result 
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public Nullable<DateTime> AlertDate { get; set; }
        public Nullable<int> IsNew { get; set; }
    }
}