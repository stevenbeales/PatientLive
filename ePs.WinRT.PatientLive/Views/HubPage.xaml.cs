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
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.ViewModels;
using Windows.System;
using ePs.WinRt.PatientLive.Views;
using System.ComponentModel;
using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;


// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class HubPage : LayoutAwarePage
    {
        private HubPageViewModel _viewModel = new HubPageViewModel();
        private User CurrentUser;

        public HubPage()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            CurrentUser = SuspensionManager.SessionState["CurrentUser"] as User;
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
            //this.Frame.SetNavigationState("1,0");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        { 
            if (e.PropertyName != null)
            {
                switch (e.PropertyName)
                {
                    case "PrivacyPolicyVisibility":
                        PrivacyPolicy.Visibility = _viewModel.PrivacyPolicyVisibility;    
                        break;
                    default:
                        break;
                }
            }
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            HubTile tile = (HubTile)e.ClickedItem;
            Type view = tile.View == "SearchMedications" ? typeof(SearchMedications) :
                       tile.View == "MyProfile" ? typeof(MyProfile) :
                       tile.View == "ResearchStudies" ? typeof(ResearchStudiesView) :
                       tile.View == "MyStudies" ? typeof(MyStudies) :
                       tile.View == "SafetyAlerts" ? typeof(SafetyAlerts) :
                       tile.View == "EducationalMaterial" ? typeof(EducationalMaterial) : 
                       tile.View == "WebPageView" ? typeof(WebPageView) :
                       typeof(HubPage);

            
            
            DataService.CreateUserAccessLog(CurrentUser, "Navigation", tile.View);

            this.Frame.Navigate(view, e.ClickedItem);
        }
    }
}