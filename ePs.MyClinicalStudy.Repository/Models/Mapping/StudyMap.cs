using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class StudyMap : EntityTypeConfiguration<Study>
    {
        public StudyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NCTID)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.Sponsor)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.EligibilityCriteriaTextblock)
                .HasMaxLength(4000);

            this.Property(t => t.EligibilityGender)
                .HasMaxLength(50);

            this.Property(t => t.EligibilityMinAge)
                .HasMaxLength(50);

            this.Property(t => t.EligibilityMaxAge)
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

            this.Property(t => t.VisitSchedule)
                .HasMaxLength(4000);

            this.Property(t => t.PrimaryCondition)
                .HasMaxLength(200);

            this.Property(t => t.Source)
                .HasMaxLength(100);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Study", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NCTID).HasColumnName("NCTID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Sponsor).HasColumnName("Sponsor");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.EligibilityCriteriaTextblock).HasColumnName("EligibilityCriteriaTextblock");
            this.Property(t => t.EligibilityGender).HasColumnName("EligibilityGender");
            this.Property(t => t.EligibilityMinAge).HasColumnName("EligibilityMinAge");
            this.Property(t => t.EligibilityMaxAge).HasColumnName("EligibilityMaxAge");
            this.Property(t => t.EligibilityMinAgeYears).HasColumnName("EligibilityMinAgeYears");
            this.Property(t => t.EligibilityMaxAgeYears).HasColumnName("EligibilityMaxAgeYears");
            this.Property(t => t.EligibilityHealthyVolunteersYN).HasColumnName("EligibilityHealthyVolunteersYN");
            this.Property(t => t.Phase).HasColumnName("Phase");
            this.Property(t => t.StudyType).HasColumnName("StudyType");
            this.Property(t => t.Enrollment).HasColumnName("Enrollment");
            this.Property(t => t.StudyStartDate).HasColumnName("StudyStartDate");
            this.Property(t => t.StudyEndDate).HasColumnName("StudyEndDate");
            this.Property(t => t.VisitSchedule).HasColumnName("VisitSchedule");
            this.Property(t => t.PrimaryCondition).HasColumnName("PrimaryCondition");
            this.Property(t => t.SurveyId).HasColumnName("SurveyId");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
        }
    }
}
