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
    public class MedTileCollection : IncrementalLoadingCollection<MedTile>
    {
        public string Conditions;
        public string Medications;
        public int UserId;

        protected override async Task<IncrementalLoadingResult<MedTile>> LoadPageItemsAsync(uint skip, uint take)
        {
            IncrementalLoadingResult<MedTile> result = null;

            try
            {
                ServiceResult<MedTile> serviceResult = await DataService.GetMedTiles(Conditions, Medications, skip, take);

                if (serviceResult != null)
                {
                    result = new IncrementalLoadingResult<MedTile>(serviceResult.Items, serviceResult.TotalItemsCount, serviceResult.GetMore);
                }
            }
            catch
            {
            }

            return result;
        }
    }
}
