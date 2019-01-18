using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.Utilities;
using Windows.UI;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.Storage;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class HubPageViewModel : BindableBase
    {
        private Visibility _privacyPolicyVisibility;

        public ObservableCollection<HubTile> HubTiles
        {
            get
            {
                var hubTiles = new ObservableCollection<HubTile>
                {
                    new HubTile { 
                                    Title      = "Search Medications", 
                                    Subtitle   = "Medications", 
                                    Image      = "/Assets/searchmedicationsIcon.png", 
                                    Alert      = SuspensionManager.SessionState["MedCount"].ToString() == "0" ? string.Empty : "New Medications: " + SuspensionManager.SessionState["MedCount"].ToString(),
                                    View       = "SearchMedications", 
                                    StartColor = HexToColorConverter.ConvertFromHex("ccc1da"), 
                                    EndColor   = HexToColorConverter.ConvertFromHex("b3a2c7"), 
                                    ColSpan    = 2,
                                    Width      = 450, 
                                    Height     = 200
                                }, 
                    new HubTile { 
                                    Title      = "My Profile", 
                                    Subtitle   = "Profile", 
                                    Image      = "/Assets/myprofileIcon.png", 
                                    View       = "MyProfile", 
                                    StartColor = HexToColorConverter.ConvertFromHex("f9b277"),
                                    EndColor   = HexToColorConverter.ConvertFromHex("ff6600"), 
                                    ColSpan    = 2, 
                                    Width      = 450, 
                                    Height     = 200 
                                },
                    new HubTile { 
                                    Title      = "Educational Material", 
                                    Subtitle   = "Educational Material", 
                                    Image      = "/Assets/educationalmaterialIcon.png",
                                    Width      = 225, 
                                    Height     = 200, 
                                    StartColor = HexToColorConverter.ConvertFromHex("ffffa3"), 
                                    EndColor   = HexToColorConverter.ConvertFromHex("ccc700"), 
                                    ColSpan    = 1,
                                    View       = "EducationalMaterial"
                                },
                    new HubTile { 
                                    Title      = "New Research", 
                                    Subtitle   = "Research", 
                                    Image      = "/Assets/newresearchIcon.png", 
                                    Alert      = SuspensionManager.SessionState["StudyCount"].ToString() == "0" ? string.Empty : "New Studies: " + SuspensionManager.SessionState["StudyCount"].ToString(), 
                                    View       = "ResearchStudies",
                                    StartColor = HexToColorConverter.ConvertFromHex("addb7b"), 
                                    EndColor   = HexToColorConverter.ConvertFromHex("80c535"), 
                                    ColSpan    = 2,
                                    Width      = 450, 
                                    Height     = 200
                                }, 
                    new HubTile { 
                                    Title      = "My Studies", 
                                    Subtitle   = "Studies", 
                                    Image      = "/Assets/savedstudiesIcon.png",
                                    View       = "MyStudies", 
                                    Alert      = string.Empty, //"Updates (1)", 
                                    Width      = 225, 
                                    Height     = 200, 
                                    StartColor = HexToColorConverter.ConvertFromHex("8eb4e3"),
                                    EndColor   = HexToColorConverter.ConvertFromHex("558ed5"), 
                                    ColSpan    = 1
                                },
                    new HubTile { 
                                    Title      = "My Health Record", 
                                    Subtitle   = "Health Record", 
                                    Image      = "/Assets/myhealthrecordIcon.png", 
                                    Width      = 225, 
                                    Height     = 200, 
                                    StartColor = HexToColorConverter.ConvertFromHex("aad8e4"),
                                    EndColor   = HexToColorConverter.ConvertFromHex("56b1ca"),
                                    ColSpan    = 1,
                                    URL        = "https://account.healthvault.com/signin",
                                    View       = "WebPageView"
                                },
                    new HubTile { 
                                    Title      = "Patient Communities", 
                                    Subtitle   = "Communities", 
                                    Image      = "/Assets/patientcommunitiesIcon.png", 
                                    Width      = 225, 
                                    Height     = 200, 
                                    StartColor = HexToColorConverter.ConvertFromHex("d8ceb0"),
                                    EndColor   = HexToColorConverter.ConvertFromHex("b6a26a"), 
                                    ColSpan    = 1,
                                    URL        = "http://www.centerwatch.com/health-resources/",
                                    View       = "WebPageView"
                                },
                    new HubTile { 
                                    Title      = "Safety Alerts", 
                                    Subtitle   = "Alerts", 
                                    Image      = "/Assets/safetyAlertsIcon.png",
                                    Width      = 450, 
                                    Height     = 200, 
                                    ColSpan    = 2,
                                    StartColor = HexToColorConverter.ConvertFromHex("fb4f57"), 
                                    EndColor   = HexToColorConverter.ConvertFromHex("d44448"), 
                                    Alert      = SuspensionManager.SessionState["AlertCount"].ToString() == "0" ? string.Empty : "New Alerts: " + SuspensionManager.SessionState["AlertCount"].ToString(),
                                    View       = "SafetyAlerts"
                                }
                };

                OnPropertyChanged("HubTiles");

                return hubTiles;
            }
        }

        public string PrivacyText
        {
            get
            { 
                return
                    "ePharmaSolutions is committed to protecting your privacy and developing technology that gives you the most powerful "
                    + "and safe online experience. This Privacy Policy applies to the ePharmaSolutions website and governs data collection and usage. By "
                    + "using the ePharmaSolutions website, you consent to the data practices described in this statement. In order to determine whether you "
                    + "qualify for a research study for which we are identifying patients, we may ask you to provide us with information about yourself or "
                    + "the patient(s) in your care. We realize that this information is intensely personal and confidential and we have created a strict Privacy "
                    + "and Security Policy ('Policy'). The information you provide will be used to determine your (or your patient's) eligibility for a research "
                    + "trial(s). Upon receiving this information, ePharmaSolutions or the physician may contact you to discuss the study in more detail, respond "
                    + "to any questions and/or schedule an appointment with the study center conducting the trial.";
            }
        }

        public BaseCommand<object> ItemViewCommand { get; set; }
        public BaseCommand<object> AgreeCommand { get; set; }
        public BaseCommand<object> CancelCommand { get; set; }

        public Visibility PrivacyPolicyVisibility
        {
            get { return _privacyPolicyVisibility; }  
            set
            {
                _privacyPolicyVisibility = value;
                OnPropertyChanged("PrivacyPolicyVisibility");
            }
        }

        public HubPageViewModel()
        {
            AgreeCommand = new BaseCommand<object>(OnAgree);
            CancelCommand = new BaseCommand<object>(OnCancel);
            //ItemViewCommand = new BaseCommand<object>(OnItemSelect);

            SetPrivacyPolicyVisibility();
        }

        protected void OnAgree(object sender)
        {
            ApplicationData.Current.RoamingSettings.Values["PPAccepted"] = "True";
            SetPrivacyPolicyVisibility();
        }

        private void SetPrivacyPolicyVisibility()
        {
            var agreed = ApplicationData.Current.RoamingSettings.Values["PPAccepted"];
            PrivacyPolicyVisibility = (agreed != null && Boolean.Parse(agreed.ToString())) ? Visibility.Collapsed : Visibility.Visible;
        }

        protected void OnCancel(object sender)
        {
            Application.Current.Exit();
        }

    }
}