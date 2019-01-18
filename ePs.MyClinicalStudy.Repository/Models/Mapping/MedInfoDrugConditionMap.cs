using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class MedInfoDrugConditionMap : EntityTypeConfiguration<MedInfoDrugCondition>
    {
        public MedInfoDrugConditionMap()
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
            this.ToTable("MedInfoDrugCondition", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MedInfoDrugId).HasColumnName("MedInfoDrugId");
            this.Property(t => t.MedInfoConditionId).HasColumnName("MedInfoConditionId");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");

            // Relationships
            this.HasRequired(t => t.MedInfoDrug)
                .WithMany(t => t.MedInfoDrugConditions)
                .HasForeignKey(d => d.MedInfoDrugId);
            this.HasRequired(t => t.MedInfoCondition)
                .WithMany(t => t.MedInfoDrugConditions)
                .HasForeignKey(d => d.MedInfoConditionId);
        }
    }
}