using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.MyClinicalStudy.Repository.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Views
{
    public class vw_SearchMedication
    {
        // View Properties
        public string DrugName { get; set; }
        public string ActiveIngredient { get; set; }
        public string Sponsors { get; set; }
        public string Information { get; set; }
        public string HREF { get; set; }
        public string Conditions { get; set; }
        public Nullable<int> IsNew { get; set; }
        public string ConditionName { get; set; }
        public Nullable<int> ConditionId { get; set; }
    }
}