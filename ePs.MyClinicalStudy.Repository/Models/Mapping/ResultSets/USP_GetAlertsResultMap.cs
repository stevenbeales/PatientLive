using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.ResultSetMapping
{
    public class USP_GetAlertsResultMap : EntityTypeConfiguration<USP_GetAlertsResult>
    {
        public USP_GetAlertsResultMap()
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

            // Column Mappings
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.AlertDate).HasColumnName("AlertDate");
            this.Property(t => t.IsNew).HasColumnName("IsNew");
        }
    }
}