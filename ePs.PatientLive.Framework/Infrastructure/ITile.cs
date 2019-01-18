using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public interface ITile
    {
        string URL { get; set; }
        string Title { get; set; }
        string Info { get; set; }
        string Subtitle { get; set; }
        Gradient Background { get; set; }
    }
}
