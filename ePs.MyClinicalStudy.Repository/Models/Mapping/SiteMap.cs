using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class SiteMap : EntityTypeConfiguration<Site>
    {
        public SiteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PILastName)
                .HasMaxLength(100);

            this.Property(t => t.PIFirstName)
                .HasMaxLength(100);

            this.Property(t => t.Address1)
                .HasMaxLength(200);

            this.Property(t => t.Address2)
                .HasMaxLength(200);

            this.Property(t => t.Address3)
                .HasMaxLength(200);

            this.Property(t => t.City)
                .HasMaxLength(200);

            this.Property(t => t.StateProvince)
                .HasMaxLength(50);

            this.Property(t => t.PostalCode)
                .HasMaxLength(50);

            this.Property(t => t.Country)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Site", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PILastName).HasColumnName("PILastName");
            this.Property(t => t.PIFirstName).HasColumnName("PIFirstName");
            this.Property(t => t.Address1).HasColumnName("Address1");
            this.Property(t => t.Address2).HasColumnName("Address2");
            this.Property(t => t.Address3).HasColumnName("Address3");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.StateProvince).HasColumnName("StateProvince");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            //this.Property(t => t.LatLong).HasColumnName("LatLong");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
        }
    }
}
