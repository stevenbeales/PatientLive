using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping.ResultSetMapping
{
    public class USP_GetNewItemCountsResultMap : EntityTypeConfiguration<USP_GetNewItemCountsResult>
    {
        public USP_GetNewItemCountsResultMap()
        {
            // Primary Key - THIS IS REQUIRED
            this.HasKey(t => t.Item);

            // Properties
            this.Property(t => t.Item)
                .HasMaxLength(50);

            // Column Mappings
            this.Property(t => t.Item).HasColumnName("Item");
            this.Property(t => t.Count).HasColumnName("Count");
        }
    }
}