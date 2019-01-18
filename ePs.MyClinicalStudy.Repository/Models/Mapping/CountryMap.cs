using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Abbreviation)
                .HasMaxLength(3);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.ISOAlpha2)
                .HasMaxLength(2);

            this.Property(t => t.ISOAlpha3)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Country", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.ISOAlpha2).HasColumnName("ISOAlpha2");
            this.Property(t => t.ISOAlpha3).HasColumnName("ISOAlpha3");
        }
    }
}