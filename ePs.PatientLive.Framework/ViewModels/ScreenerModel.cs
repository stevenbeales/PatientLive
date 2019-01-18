using ePs.PatientLive.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class ScreenerModel
    {
        public StudyTile StudyTile { get; set; }
        public Contact SitePrimaryContact { get; set; }
        public Contact SiteBackupContact { get; set; }
        public Contact StudyPrimaryContact { get; set; }
        public Contact StudyBackupContact { get; set; }
        public string SitePrimaryContactString { get; set; }
        public string SiteBackupContactString { get; set; }
        public string StudyPrimaryContactString { get; set; }
        public string StudyBackupContactString { get; set; }

        public ScreenerModel(StudyTile tile)
        {
            StudyTile = tile;
            SitePrimaryContactString = GetContactString(StudyTile.SitePrimaryContact ?? string.Empty);     
            SiteBackupContactString = GetContactString(StudyTile.SiteBackupContact ?? string.Empty);         
            StudyPrimaryContactString = GetContactString(StudyTile.StudyPrimaryContact ?? string.Empty);
            StudyBackupContactString = GetContactString(StudyTile.StudyBackupContact ?? string.Empty);   
        }

        private string GetContactString(string contact)
        {
            string[] stringSeparator = new string[] { "::" };
            var contactString = string.Empty;
            if (contact != null && contact.Length > 0)
            {
                var contactArray = contact.Split(stringSeparator, StringSplitOptions.None);
                for (int i = 0; i < contactArray.Length; i++)
                {
                    switch(i)
                    {
                        case 0:
                            if (contactArray[i].Trim().Length > 0)
                                contactString += "Name: " + contactArray[i] + ", ";
                            break;
                        case 1:
                            if (contactArray[i].Trim().Length > 0)
                                contactString += "Email: " + contactArray[i] + ", ";
                            break;
                        case 2:
                            if (contactArray[i].Trim().Length > 0)
                                contactString += "Phone: " + contactArray[i] + ", ";
                            break;
                    }
                }
            }
            return contactString;
        }

        private string GetContactString(Contact contact)
        {
            var contactString = (contact.Name.Length > 0 ? "Name: " + contact.Name + " " : string.Empty) +
                (contact.Phone.Length > 0 ? "Phone: " + contact.Phone + " " : string.Empty + " ") +
                (contact.Email.Length > 0 ? "Email: " + contact.Email : string.Empty);

            return contactString;
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
