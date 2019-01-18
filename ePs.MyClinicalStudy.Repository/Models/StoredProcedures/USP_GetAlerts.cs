using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.StoredProcedures
{
    public class USP_GetAlerts : IStoredProcedure<USP_GetAlertsResult>
    {
        // Stored Procedure Parameters
        public Nullable<int> user_id { get; set; }
    }
}