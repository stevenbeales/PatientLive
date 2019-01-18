using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Utilities
{
    public class IncrementalLoadingCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Number of items loaded in current page.
        /// </summary>
        public uint ItemsCount { get; set; }

        /// <summary>
        /// Total number of items of the collection.
        /// </summary>
        public uint TotalItemsCount { get; set; }

        public IncrementalLoadingCompletedEventArgs(uint itemsCount, uint totalItemsCount)
        {
            ItemsCount = itemsCount;
            TotalItemsCount = totalItemsCount;
        }
    }
}
