using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Live;
using Microsoft.Live.Controls;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ApplicationSettings;
using ePs.WinRt.PatientLive.Views;
using ePs.PatientLive.Framework.ViewModels;
using ePs.PatientLive.Framework.Infrastructure;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ePs.WinRT.PatientLive.Views.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterPage : Page
    {
        private static string PrivacyText = "ePharmaSolutions is committed to protecting your privacy and developing technology that gives you the most powerful "
             + "and safe online experience. This Privacy Policy applies to the ePharmaSolutions website and governs data collection and usage. By "
            + "using the ePharmaSolutions website, you consent to the data practices described in this statement. In order to determine whether you "
            + "qualify for a research study for which we are identifying patients, we may ask you to provide us with information about yourself or "
            + "the patient(s) in your care. We realize that this information is intensely personal and confidential and we have created a strict Privacy "
            + "and Security Policy ('Policy'). The information you provide will be used to determine your (or your patient's) eligibility for a research "
            + "trial(s). Upon receiving this information, ePharmaSolutions or the physician may contact you to discuss the study in more detail, respond "
            + "to any questions and/or schedule an appointment with the study center conducting the trial";

        public MasterPage()
        {
            this.InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Enabled;
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;
            PrivacyStatement.Text = PrivacyText;
            //ApplicationData.Current.LocalSettings.Values["PPAccepted"] = "False";
        }

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("s", "Privacy Statement", (p) => { cfoSettings.IsOpen = true; }));
        } 

        Page rootPage = null;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = e.Parameter as Page;
            frame1.Navigate(typeof(HubPage), this);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (frame1.CanGoBack)
            {
                frame1.GoBack();
            }
            else if (rootPage != null && rootPage.Frame.CanGoBack)
            {
                rootPage.Frame.GoBack();
            }
        }

        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            //frame1.Navigate(typeof(Page1), this);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(typeof(HubPage), this);
            TopAppBar.IsOpen = false;
        }

        private void TopBarBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            var param = b.CommandParameter as string;
            
            var type = param == "SearchMeds" ? typeof(SearchMedications) :
                param == "Profile" ? typeof(MyProfile) :
                param == "Studies" ? typeof(ResearchStudiesView) :
                param == "MyStudies" ? typeof(MyStudies) :
                param == "Education" ? typeof(EducationalMaterial) :
                param == "Alerts" ? typeof(SafetyAlerts) : null
                ;

            if (type == null)
            {
                HubTile tile;
                if (param == "Health")
                    tile =
                new HubTile
                {
                    Title = "My Health Record",
                    Subtitle = "Health Record",
                    Image = "/Assets/myhealthrecordIcon.png",
                    Width = 225,
                    Height = 200,
                    ColSpan = 1,
                    URL = "https://account.healthvault.com/signin"
                };
                else
                    tile =
                new HubTile
                {
                    Title = "Patient Communities",
                    Subtitle = "Communities",
                    Image = "/Assets/patientcommunitiesIcon.png",
                    Width = 225,
                    Height = 200,

                    ColSpan = 1,
                    URL = "http://www.centerwatch.com/health-resources/"
                };

                frame1.Navigate(typeof(WebPageView), tile);
                
            }
            else
            {
                frame1.Navigate(type);
            }
            TopAppBar.IsOpen = false;
        }

    }
}
