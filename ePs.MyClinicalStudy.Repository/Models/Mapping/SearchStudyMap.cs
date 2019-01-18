using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.Views;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class SearchStudyMap : EntityTypeConfiguration<SearchStudy>
    {
        public SearchStudyMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.NCTID);

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
            this.Property(t => t.SiteName)
                .HasMaxLength(200);
            this.Property(t => t.PILastName)
                .HasMaxLength(200);
            this.Property(t => t.PIFirstName)
                .HasMaxLength(200);
            this.Property(t => t.Address1)
                .HasMaxLength(200);
            this.Property(t => t.Address2)
                .HasMaxLength(200);
            this.Property(t => t.Address3)
                .HasMaxLength(200);
            this.Property(t => t.City)
                .HasMaxLength(200);
            this.Property(t => t.StateProvince)
                .HasMaxLength(200);
            this.Property(t => t.PostalCode)
                .HasMaxLength(200);
            this.Property(t => t.Country)
                .HasMaxLength(200);
            this.Property(t => t.SiteStatus)
                .HasMaxLength(200);
            this.Property(t => t.SitePrimaryContact)
                .HasMaxLength(608);
            this.Property(t => t.SiteBackupContact)
                .HasMaxLength(608);

            // View & Column Mappings
            this.ToTable("SearchStudies", "MCS");
            this.Property(t => t.StudyId).HasColumnName("StudyId");
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
            this.Property(t => t.StudyPrimaryContact).HasColumnName("StudyPrimaryContact");
            this.Property(t => t.StudyBackupContact).HasColumnName("StudyBackupContact");
            this.Property(t => t.SiteName).HasColumnName("SiteName");
            this.Property(t => t.PILastName).HasColumnName("PILastName");
            this.Property(t => t.PIFirstName).HasColumnName("PIFirstName");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Address3).HasColumnName("Address3");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.StateProvince).HasColumnName("StateProvince");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.SiteStatus).HasColumnName("SiteStatus");
            this.Property(t => t.SitePrimaryContact).HasColumnName("SitePrimaryContact");
            this.Property(t => t.SiteBackupContact).HasColumnName("SiteBackupContact");
        }
    }
}