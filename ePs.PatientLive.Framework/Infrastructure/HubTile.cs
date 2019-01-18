using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class HubTile : ITile
    {
        public string Subtitle { get; set; }

        public string Image { get; set; }

        public string View { get; set; }

        public string Alert { get; set; }

        public Color StartColor { get; set; }

        public Color EndColor { get; set; }

        public int ColSpan { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string URL { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public Gradient Background { get; set; }
    }
}