using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.ResultSetMapping
{
    public class USP_SearchStudiesResultMap : EntityTypeConfiguration<USP_SearchStudiesResult>
    {
        public USP_SearchStudiesResultMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.RN);

            // Properties
            this.Property(t => t.NCTID)
                .HasMaxLength(50);
            this.Property(t => t.Title)
                .HasMaxLength(500);
            this.Property(t => t.Sponsor)
                .HasMaxLength(200);
            this.Property(t => t.StudyStatus)
                .HasMaxLength(50);
            this.Property(t => t.EligibilityCriteriaTextblock)
                .HasMaxLength(4000);
            this.Property(t => t.EligibilityGender)
                .HasMaxLength(50);
            this.Property(t => t.EligibilityMinAgeYears)
                .HasColumnType("tinyint");
            this.Property(t => t.EligibilityMaxAgeYears)
                .HasColumnType("tinyint");
            this.Property(t => t.EligibilityHealthyVolunteersYN)
                .HasMaxLength(50);
            this.Property(t => t.Phase)
                .HasMaxLength(50);
            this.Property(t => t.StudyType)
                .HasMaxLength(50);
            this.Property(t => t.Conditions)
                .HasMaxLength(4000);
            this.Property(t => t.StudyPrimaryContact)
                .HasMaxLength(1000);
            this.Property(t => t.StudyBackupContact)
                .HasMaxLength(1000);
            this.Property(t => t.SitePrimaryContact)
                .HasMaxLength(1000);
            this.Property(t => t.SiteBackupContact)
                .HasMaxLength(1000);
            this.Property(t => t.City)
                .HasMaxLength(200);

            // Column Mappings
            this.Property(t => t.RN).HasColumnName("RN");
            this.Property(t => t.StudyId).HasColumnName("StudyId");
            this.Property(t => t.SiteId).HasColumnName("SiteId");
            this.Property(t => t.NCTID).HasColumnName("NCTID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Sponsor).HasColumnName("Sponsor");
            this.Property(t => t.StudyStatus).HasColumnName("StudyStatus");
            this.Property(t => t.EligibilityCriteriaTextblock).HasColumnName("EligibilityCriteriaTextblock");
            this.Property(t => t.EligibilityGender).HasColumnName("EligibilityGender");
            this.Property(t => t.EligibilityMinAgeYears).HasColumnName("EligibilityMinAgeYears");
            this.Property(t => t.EligibilityMaxAgeYears).HasColumnName("EligibilityMaxAgeYears");
            this.Property(t => t.EligibilityHealthyVolunteersYN).HasColumnName("EligibilityHealthyVolunteersYN");
            this.Property(t => t.Phase).HasColumnName("Phase");
            this.Property(t => t.StudyType).HasColumnName("StudyType");
            this.Property(t => t.Enrollment).HasColumnName("Enrollment");
            this.Property(t => t.StudyStartDate).HasColumnName("StudyStartDate");
            this.Property(t => t.StudyEndDate).HasColumnName("StudyEndDate");
            this.Property(t => t.Conditions).HasColumnName("Conditions");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.StudyPrimaryContact).HasColumnName("StudyPrimaryContact");
            this.Property(t => t.StudyBackupContact).HasColumnName("StudyBackupContact");
            this.Property(t => t.SitePrimaryContact).HasColumnName("SitePrimaryContact");
            this.Property(t => t.SiteBackupContact).HasColumnName("SiteBackupContact");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
            this.Property(t => t.TotalItems).HasColumnName("TotalItems");
        }
    }
}