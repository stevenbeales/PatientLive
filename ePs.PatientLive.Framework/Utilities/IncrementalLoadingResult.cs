using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Utilities
{
    public class IncrementalLoadingResult<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public uint TotalItemsCount { get; private set; }
        public bool GetMore { get; set; }

        public IncrementalLoadingResult(IEnumerable<T> items, uint totalItemsCount, bool? getMore)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
            GetMore = getMore ?? false;
        }
    }
}
