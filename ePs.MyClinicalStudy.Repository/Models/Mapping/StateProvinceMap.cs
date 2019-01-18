using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class StateProvinceMap : EntityTypeConfiguration<StateProvince>
    {
        public StateProvinceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Abbreviation)
                .HasMaxLength(5);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("StateProvince", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.CountryStateProvinces)
                .HasForeignKey(d => d.CountryId);
        }
    }
}