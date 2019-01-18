using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class SafetyTileCollection : IncrementalLoadingCollection<SafetyTile>
    {
        public int UserId;

        protected override async Task<IncrementalLoadingResult<SafetyTile>> LoadPageItemsAsync(uint skip, uint take)
        {
            IncrementalLoadingResult<SafetyTile> result = null;

            try
            {
                ServiceResult<SafetyTile> serviceResult = await DataService.GetSafetyAlerts(UserId, skip, take);

                if (serviceResult != null)
                {
                    result = new IncrementalLoadingResult<SafetyTile>(serviceResult.Items, serviceResult.TotalItemsCount, serviceResult.GetMore);
                }
            }
            catch
            {
            }

            return result;
        }

    }
}
