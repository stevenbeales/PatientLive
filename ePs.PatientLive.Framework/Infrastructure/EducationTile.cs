using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class EducationTile : ITile
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Subtitle { get; set; }
        public int Id { get; set; }
        public Gradient Background { get; set; }
    }
}