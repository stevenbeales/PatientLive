using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class UserAccessLogMap : EntityTypeConfiguration<UserAccessLog>
    {
        public UserAccessLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TileName)
                .HasMaxLength(50);

            this.Property(t => t.Activity)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserAccessLog", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.TileName).HasColumnName("TileName");
            this.Property(t => t.Activity).HasColumnName("Activity");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserAccessLogs)
                .HasForeignKey(d => d.User_Id);

        }
    }
}