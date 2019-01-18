using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ePs.MyClinicalStudy.Repository.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.user_id);

            // Properties
            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.user_password)
                .HasMaxLength(50);

            this.Property(t => t.password_hint)
                .HasMaxLength(100);

            this.Property(t => t.first_name)
                .HasMaxLength(255);

            this.Property(t => t.last_name)
                .HasMaxLength(255);

            this.Property(t => t.email_address)
                .HasMaxLength(255);

            this.Property(t => t.current_position)
                .HasMaxLength(255);

            this.Property(t => t.company)
                .HasMaxLength(255);

            this.Property(t => t.location)
                .HasMaxLength(255);

            this.Property(t => t.custom_data_1)
                .HasMaxLength(255);

            this.Property(t => t.custom_data_2)
                .HasMaxLength(255);

            this.Property(t => t.custom_data_3)
                .HasMaxLength(255);

            this.Property(t => t.active_yn)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.user_title)
                .HasMaxLength(255);

            this.Property(t => t.company_head)
                .HasMaxLength(255);

            this.Property(t => t.department)
                .HasMaxLength(255);

            this.Property(t => t.address_1)
                .HasMaxLength(255);

            this.Property(t => t.address_2)
                .HasMaxLength(255);

            this.Property(t => t.zip_code)
                .HasMaxLength(100);

            this.Property(t => t.city)
                .HasMaxLength(255);

            this.Property(t => t.state)
                .HasMaxLength(100);

            this.Property(t => t.country)
                .HasMaxLength(255);

            this.Property(t => t.business_phone_1)
                .HasMaxLength(50);

            this.Property(t => t.business_phone_2)
                .HasMaxLength(50);

            this.Property(t => t.business_fax)
                .HasMaxLength(50);

            this.Property(t => t.mobile_phone)
                .HasMaxLength(50);

            this.Property(t => t.therapeutic_area)
                .HasMaxLength(255);

            this.Property(t => t.secondary_specialty)
                .HasMaxLength(2000);

            this.Property(t => t.practice_type)
                .HasMaxLength(255);

            this.Property(t => t.primary_specialty)
                .HasMaxLength(255);

            this.Property(t => t.WindowsLiveID)
                .HasMaxLength(100);

            this.Property(t => t.Gender)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("sur_user");
            this.Property(t => t.user_id).HasColumnName("user_id");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.register_date).HasColumnName("register_date");
            this.Property(t => t.user_password).HasColumnName("user_password");
            this.Property(t => t.password_hint).HasColumnName("password_hint");
            this.Property(t => t.first_name).HasColumnName("first_name");
            this.Property(t => t.last_name).HasColumnName("last_name");
            this.Property(t => t.email_address).HasColumnName("email_address");
            this.Property(t => t.current_position).HasColumnName("current_position");
            this.Property(t => t.company).HasColumnName("company");
            this.Property(t => t.location).HasColumnName("location");
            this.Property(t => t.custom_data_1).HasColumnName("custom_data_1");
            this.Property(t => t.custom_data_2).HasColumnName("custom_data_2");
            this.Property(t => t.custom_data_3).HasColumnName("custom_data_3");
            this.Property(t => t.active_yn).HasColumnName("active_yn");
            this.Property(t => t.user_title).HasColumnName("user_title");
            this.Property(t => t.company_head).HasColumnName("company_head");
            this.Property(t => t.department).HasColumnName("department");
            this.Property(t => t.address_1).HasColumnName("address_1");
            this.Property(t => t.address_2).HasColumnName("address_2");
            this.Property(t => t.zip_code).HasColumnName("zip_code");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.state).HasColumnName("state");
            this.Property(t => t.country).HasColumnName("country");
            this.Property(t => t.business_phone_1).HasColumnName("business_phone_1");
            this.Property(t => t.business_phone_2).HasColumnName("business_phone_2");
            this.Property(t => t.business_fax).HasColumnName("business_fax");
            this.Property(t => t.mobile_phone).HasColumnName("mobile_phone");
            this.Property(t => t.active_until).HasColumnName("active_until");
            this.Property(t => t.therapeutic_area).HasColumnName("therapeutic_area");
            this.Property(t => t.secondary_specialty).HasColumnName("secondary_specialty");
            this.Property(t => t.practice_type).HasColumnName("practice_type");
            this.Property(t => t.filed_1572).HasColumnName("filed_1572");
            this.Property(t => t.primary_specialty).HasColumnName("primary_specialty");
            this.Property(t => t.WindowsLiveID).HasColumnName("WindowsLiveID");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
        }
    }
}