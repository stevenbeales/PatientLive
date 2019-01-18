using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class MedTile : ITile
    {

        public string TradeName { get; set; }

        public string GenericName { get; set; }

        public string Manufacturer { get; set; }

        public string Url { get; set; }

        public int TotalItems { get; set; }

        public string Info { get; set; }

        public string Title
        {
            get;
            set;
        }



        public string URL
        {
            get;
            set;
        }

        public string Subtitle { get; set; }

        public Gradient Background { get; set; }

        }
}
