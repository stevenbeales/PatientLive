using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class UserSearchMap : EntityTypeConfiguration<UserSearch>
    {
        public UserSearchMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Conditions)
                .HasMaxLength(4000);

            this.Property(t => t.Medications)
                .HasMaxLength(4000);

            this.Property(t => t.Gender)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PostalCode)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserSearch", "MCS");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
            this.Property(t => t.Conditions).HasColumnName("Conditions");
            this.Property(t => t.Medications).HasColumnName("Medications");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserSearches)
                .HasForeignKey(d => d.User_Id);

        }
    }
}
