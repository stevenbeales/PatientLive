using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class SafetyTile : ITile
    {

        public string URL
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Info
        {
            get;
            set;
        }

        public string Subtitle
        {
            get;
            set;
        }

        public DateTime? AlertDate { get; set; }

        public bool IsNew { get; set; }

        public Gradient Background { get; set; }
    }
}
