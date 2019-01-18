using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class EducationalMaterialsFAQMap : EntityTypeConfiguration<EducationalMaterialsFAQ>
    {
        public EducationalMaterialsFAQMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Question)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Answer)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EducationalMaterialsFAQ", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Question).HasColumnName("Question");
            this.Property(t => t.Answer).HasColumnName("Answer");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
        }
    }
}
