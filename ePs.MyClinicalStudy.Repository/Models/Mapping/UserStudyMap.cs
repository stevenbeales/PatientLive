using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class UserStudyMap : EntityTypeConfiguration<UserStudy>
    {
        public UserStudyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserStudy", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.StudyId).HasColumnName("StudyId");
            this.Property(t => t.SiteId).HasColumnName("SiteId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserStudies)
                .HasForeignKey(d => d.User_Id);
            this.HasRequired(t => t.Study)
                .WithMany(t => t.UserStudies)
                .HasForeignKey(d => d.StudyId);
            this.HasRequired(t => t.Site)
                .WithMany(t => t.UserStudies)
                .HasForeignKey(d => d.SiteId);
        }
    }
}