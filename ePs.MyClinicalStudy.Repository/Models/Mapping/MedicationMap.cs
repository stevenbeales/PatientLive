using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class MedicationMap : EntityTypeConfiguration<Medication>
    {
        public MedicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Source)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Medication", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
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
