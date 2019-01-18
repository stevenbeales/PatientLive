using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.ResultSetMapping
{
    public class USP_SearchMedicationsResultMap : EntityTypeConfiguration<USP_SearchMedicationsResult>
    {
        public USP_SearchMedicationsResultMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.DrugName);

            // Properties
            this.Property(t => t.DrugName)
                .HasMaxLength(150);
            this.Property(t => t.Information)
                .HasMaxLength(4000);
            this.Property(t => t.HREF)
                .HasMaxLength(2000);
            this.Property(t => t.Conditions)
                .HasMaxLength(100);

            // Column Mappings
            this.Property(t => t.DrugName).HasColumnName("DrugName");
            this.Property(t => t.Information).HasColumnName("Information");
            this.Property(t => t.HREF).HasColumnName("HREF");
            this.Property(t => t.Conditions).HasColumnName("Conditions");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
            this.Property(t => t.TotalItems).HasColumnName("TotalItems");
        }
    }
}