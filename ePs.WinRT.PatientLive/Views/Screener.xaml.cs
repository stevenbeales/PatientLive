using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ePs.PatientLive.Framework.Infrastructure;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.ViewModels;
using ePs.PatientLive.Framework.DataModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Screener : LayoutAwarePage
    {
        private StudyTile tile;
        public ScreenerModel ViewModel { get; set; }
        
        public Screener()
        {
            this.InitializeComponent();

            
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (SuspensionManager.SessionState.ContainsKey("StudyTile") && SuspensionManager.SessionState["StudyTile"] != null)
            {
                tile = SuspensionManager.SessionState["StudyTile"] as StudyTile;
                SuspensionManager.SessionState["StudyTile"] = null;
            }
            else
            {
                tile = (StudyTile)e.Parameter;
            }

            ViewModel = new ScreenerModel(tile);
            this.DataContext = ViewModel;

            ScreenerButton.Visibility = tile.URL.Length > 0 ? Visibility.Visible : Visibility.Collapsed;

            if (SuspensionManager.SessionState.ContainsKey("ShowContactInfo"))
            {
                var showContactInfo = false;
                showContactInfo = Boolean.Parse(SuspensionManager.SessionState["ShowContactInfo"].ToString());
                if (showContactInfo)
                {
                    EligibilityPanel.Visibility = Visibility.Collapsed;
                    ContactPanel.Visibility = Visibility.Visible;
                    toggleSwitch1.IsOn = true;
                    SuspensionManager.SessionState["ShowContactInfo"] = false;               
                }
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = (ToggleSwitch)sender;
            if (toggle.IsOn)
            {
                var currentUser = SuspensionManager.SessionState["CurrentUser"] as User;
                DataService.CreateUserAccessLog(currentUser, "Eligibility Acknowledgment::" + ViewModel.StudyTile.StudyId, "ResearchStudies");
                
                if (currentUser.UserConditions.Where(o => o.Deleted == null).Count() <= 0)
                {
                    SuspensionManager.SessionState["StudyTile"] = tile;
                    this.Frame.Navigate(typeof(EditProfile));
                }
                else
                {   
                    EligibilityPanel.Visibility = Visibility.Collapsed;
                    ContactPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                EligibilityPanel.Visibility = Visibility.Visible;
                ContactPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ScreenerButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (Button)e.OriginalSource;
            Windows.System.Launcher.LaunchUriAsync(new Uri(link.Tag.ToString()));
        }
    }
}