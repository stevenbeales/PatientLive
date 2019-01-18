using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.Views;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.Views
{
    public class vw_GetAlertMap : EntityTypeConfiguration<vw_GetAlert>
    {
        public vw_GetAlertMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.Title);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);
            this.Property(t => t.Description)
                .HasMaxLength(4000);
            this.Property(t => t.URL)
                .HasMaxLength(2000);

            // View & Column Mappings
            this.ToTable("vw_GetAlerts", "MCS");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.AlertDate).HasColumnName("AlertDate");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
        }
    }
}