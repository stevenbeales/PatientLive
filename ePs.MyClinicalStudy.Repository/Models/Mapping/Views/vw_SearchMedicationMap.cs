using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.Views;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.Views
{
    public class vw_SearchMedicationMap : EntityTypeConfiguration<vw_SearchMedication>
    {
        public vw_SearchMedicationMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.DrugName);

            // Properties
            this.Property(t => t.DrugName)
                .HasMaxLength(150);
            this.Property(t => t.ActiveIngredient)
                .HasMaxLength(250);
            this.Property(t => t.Sponsors)
                .HasMaxLength(4000);
            this.Property(t => t.Information)
                .HasMaxLength(4000);
            this.Property(t => t.HREF)
                .HasMaxLength(2000);
            this.Property(t => t.Conditions)
                .HasMaxLength(4000);
            this.Property(t => t.ConditionName)
                .HasMaxLength(150);

            // View & Column Mappings
            this.ToTable("vw_SearchMedications", "MCS");
            this.Property(t => t.DrugName).HasColumnName("DrugName");
            this.Property(t => t.ActiveIngredient).HasColumnName("ActiveIngredient");
            this.Property(t => t.Sponsors).HasColumnName("Sponsors");
            this.Property(t => t.Information).HasColumnName("Information");
            this.Property(t => t.HREF).HasColumnName("HREF");
            this.Property(t => t.Conditions).HasColumnName("Conditions");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
            this.Property(t => t.ConditionName).HasColumnName("ConditionName");
            this.Property(t => t.ConditionId).HasColumnName("ConditionId");
        }
    }
}