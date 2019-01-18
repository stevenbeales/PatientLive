using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Utilities
{
    public class ServiceResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public uint TotalItemsCount { get; set; }
        public bool GetMore { get; set; }

        public ServiceResult()
        {
            GetMore = false;
        }
    }
}
