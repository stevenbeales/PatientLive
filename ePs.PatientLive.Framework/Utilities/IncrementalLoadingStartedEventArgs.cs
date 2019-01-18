using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Utilities
{
    public class IncrementalLoadingStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Number of items to load in the current page.
        /// </summary>
        public uint PageSize { get; set; }

        /// <summary>
        /// Page lower bound over the total number of items.
        /// </summary>
        public uint PageLowerBound { get; set; }

        /// <summary>
        /// Page upper bound over the total number of items.
        /// </summary>
        public uint PageUpperBound { get; set; }

        public IncrementalLoadingStartedEventArgs(uint pageSize, uint pageLowerBound, uint pageUpperBound)
        {
            PageSize = pageSize;
            PageLowerBound = pageLowerBound;
            PageUpperBound = pageUpperBound;
        }
    }
}
