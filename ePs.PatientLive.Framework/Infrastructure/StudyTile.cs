using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class StudyTile : ITile
    {
        public Gradient Background { get; set; }
        public string Title { get; set; }
        public string Condition { get; set; }
        public string City { get; set; }
        public int? Distance { get; set; }
        public string Eligibility { get; set; }
        public string ContactInfo { get; set; }
        public string URL { get; set; }
        public string Gender { get; set; }
        public string HealthyVolunteersYN { get; set; }
        public byte? MaxAge { get; set; }
        public byte? MinAge { get; set; }
        public int? Enrollment { get; set; }
        public string Phase { get; set; }
        public string SiteBackupContact { get; set; }
        public string SitePrimaryContact { get; set; }
        public string Sponsor { get; set; }
        public string StudyBackupContact { get; set; }
        public string StudyPrimaryContact { get; set; }
        public DateTime? StudyEndDate { get; set; }
        public string Status { get; set; }
        public string StudyType { get; set; }
        public int? SurveyId { get; set; }
        public int? TotalItems { get; set; }
        public string Info { get; set; }
        public string Subtitle { get; set; }
        public int StudyId { get; set; }
        public int? IsNew { get; set; }
        public int? SiteId { get; set; }
    }
}