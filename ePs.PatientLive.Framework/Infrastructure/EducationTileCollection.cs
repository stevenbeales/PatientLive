using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.DataModel;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class EducationTileCollection : IncrementalLoadingCollection<EducationTile>
    {
        protected override async Task<IncrementalLoadingResult<EducationTile>> LoadPageItemsAsync(uint skip, uint take)
        {
            IncrementalLoadingResult<EducationTile> result = null;

            try
            {
                ServiceResult<EducationTile> serviceResult = await DataService.GetEducationFAQ(skip, take);

                if (serviceResult != null)
                {
                    result = new IncrementalLoadingResult<EducationTile>(serviceResult.Items, serviceResult.TotalItemsCount, false);
                }
            }
            catch
            {
            }

            return result;
        }
    }
}
