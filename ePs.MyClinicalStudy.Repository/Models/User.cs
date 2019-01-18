using System;
using System.Collections.Generic;

namespace ePs.MyClinicalStudy.Repository.Models
{
    public class User
    {
        public User()
        {
            this.UserAccessLogs = new List<UserAccessLog>();
            this.UserConditions = new List<UserCondition>();
            this.UserMedications = new List<UserMedication>();
            this.UserSearches = new List<UserSearch>();
            this.UserStudies = new List<UserStudy>();
        }

        public int user_id { get; set; }
        public string username { get; set; }
        public Nullable<System.DateTime> register_date { get; set; }
        public string user_password { get; set; }
        public string password_hint { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public string current_position { get; set; }
        public string company { get; set; }
        public string location { get; set; }
        public string custom_data_1 { get; set; }
        public string custom_data_2 { get; set; }
        public string custom_data_3 { get; set; }
        public string active_yn { get; set; }
        public string user_title { get; set; }
        public string company_head { get; set; }
        public string department { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string zip_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string business_phone_1 { get; set; }
        public string business_phone_2 { get; set; }
        public string business_fax { get; set; }
        public string mobile_phone { get; set; }
        public Nullable<System.DateTime> active_until { get; set; }
        public string therapeutic_area { get; set; }
        public string secondary_specialty { get; set; }
        public string practice_type { get; set; }
        public Nullable<int> filed_1572 { get; set; }
        public string primary_specialty { get; set; }
        public string WindowsLiveID { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
        public virtual ICollection<UserAccessLog> UserAccessLogs { get; set; }
        public virtual ICollection<UserCondition> UserConditions { get; set; }
        public virtual ICollection<UserMedication> UserMedications { get; set; }
        public virtual ICollection<UserSearch> UserSearches { get; set; }
        public virtual ICollection<UserStudy> UserStudies { get; set; }
    }
}