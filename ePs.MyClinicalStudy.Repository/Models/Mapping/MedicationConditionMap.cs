using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class MedicationConditionMap : EntityTypeConfiguration<MedicationCondition>
    {
        public MedicationConditionMap()
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
            this.ToTable("MedicationCondition", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MedicationId).HasColumnName("MedicationId");
            this.Property(t => t.ConditionId).HasColumnName("ConditionId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");

            // Relationships
            this.HasRequired(t => t.Condition)
                .WithMany(t => t.MedicationConditions)
                .HasForeignKey(d => d.ConditionId);
            this.HasRequired(t => t.Medication)
                .WithMany(t => t.MedicationConditions)
                .HasForeignKey(d => d.MedicationId);

        }
    }
}
