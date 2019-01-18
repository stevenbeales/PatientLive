using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class MyStudyTileCollection : IncrementalLoadingCollection<StudyTile>
    {
        public int UserId;

        protected override async Task<IncrementalLoadingResult<StudyTile>> LoadPageItemsAsync(uint skip, uint take)
        {
            IncrementalLoadingResult<StudyTile> result = null;

            try
            {
                ServiceResult<StudyTile> serviceResult = null;
                if (serviceResult != null)
                {
                    result = new IncrementalLoadingResult<StudyTile>(serviceResult.Items, serviceResult.TotalItemsCount, serviceResult.GetMore);
                }
            }
            catch
            {
            }

            return result;
        }

    }
}
