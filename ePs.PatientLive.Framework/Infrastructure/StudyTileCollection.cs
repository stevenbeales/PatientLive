using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class StudyTileCollection : IncrementalLoadingCollection<StudyTile>
    {
        public string dob;
        public string gender;
        public string conditions;
        public string postalCode;
        public string country;
        public Nullable<int> distance;
        public Nullable<decimal> latitude;
        public Nullable<decimal> longitude;
        public int userId;
        
        protected override async Task<IncrementalLoadingResult<StudyTile>> LoadPageItemsAsync(uint skip, uint take)
        {
            IncrementalLoadingResult<StudyTile> result = null;

            try
            {
                ServiceResult<StudyTile> serviceResult = await DataService.GetStudyTiles(dob, gender, conditions, postalCode, country, distance, latitude, longitude, skip, take, userId);

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
