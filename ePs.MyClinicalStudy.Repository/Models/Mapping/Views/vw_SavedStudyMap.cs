using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.Views;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.Views
{
    public class vw_SavedStudyMap : EntityTypeConfiguration<vw_SavedStudy>
    {
        public vw_SavedStudyMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.UserStudyId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);
            this.Property(t => t.EligibilityGender)
                .HasMaxLength(50);
            this.Property(t => t.EligibilityHealthyVolunteersYN)
                .HasMaxLength(50);
            this.Property(t => t.EligibilityMinAgeYears)
                .HasColumnType("tinyint");
            this.Property(t => t.EligibilityMaxAgeYears)
                .HasColumnType("tinyint");
            this.Property(t => t.Phase)
                .HasMaxLength(50);
            this.Property(t => t.SiteBackupContact)
                .HasMaxLength(608);
            this.Property(t => t.SitePrimaryContact)
                .HasMaxLength(608);
            this.Property(t => t.Sponsor)
                .HasMaxLength(200);
            this.Property(t => t.StudyBackupContact)
                .HasMaxLength(408);
            this.Property(t => t.StudyStatus)
                .HasMaxLength(50);
            this.Property(t => t.StudyType)
                .HasMaxLength(50);
            this.Property(t => t.City)
                   .HasMaxLength(200);

            // View & Column Mappings
            this.ToTable("vw_SavedStudies", "MCS");
            this.Property(t => t.UserStudyId).HasColumnName("UserStudyId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Conditions).HasColumnName("Conditions");
            this.Property(t => t.EligibilityCriteriaTextblock).HasColumnName("EligibilityCriteriaTextblock");
            this.Property(t => t.EligibilityGender).HasColumnName("EligibilityGender");
            this.Property(t => t.EligibilityHealthyVolunteersYN).HasColumnName("EligibilityHealthyVolunteersYN");
            this.Property(t => t.EligibilityMinAgeYears).HasColumnName("EligibilityMinAgeYears");
            this.Property(t => t.EligibilityMaxAgeYears).HasColumnName("EligibilityMaxAgeYears");
            this.Property(t => t.Enrollment).HasColumnName("Enrollment");
            this.Property(t => t.Phase).HasColumnName("Phase");
            this.Property(t => t.SiteBackupContact).HasColumnName("SiteBackupContact");
            this.Property(t => t.SitePrimaryContact).HasColumnName("SitePrimaryContact");
            this.Property(t => t.Sponsor).HasColumnName("Sponsor");
            this.Property(t => t.StudyBackupContact).HasColumnName("StudyBackupContact");
            this.Property(t => t.StudyEndDate).HasColumnName("StudyEndDate");
            this.Property(t => t.StudyStatus).HasColumnName("StudyStatus");
            this.Property(t => t.StudyType).HasColumnName("StudyType");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.StudyId).HasColumnName("StudyId");
            this.Property(t => t.SiteId).HasColumnName("SiteId");
            this.Property(t => t.City).HasColumnName("City");
        }
    }
}