using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.ViewModels;
using ePs.WinRT.PatientLive.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace ePs.WinRt.PatientLive.Views
{
    
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class SafetyAlerts : LayoutAwarePage
    {
        private SafetyAlertsModel ViewModel { get; set; }

        public SafetyAlerts()
        {
            this.InitializeComponent();
            ViewModel = new SafetyAlertsModel();
            this.DataContext = ViewModel;
        }

        private void Items_LoadingStarted(object sender, IncrementalLoadingStartedEventArgs e)
        {
            ProgressStatus.Visibility = Visibility.Visible;
        }

        private void Items_LoadingCompleted(object sender, IncrementalLoadingCompletedEventArgs e)
        {
            ProgressStatus.Visibility = Visibility.Collapsed;
            itemGridView.ItemsSource = ViewModel.Items;
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
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var tile = (SafetyTile)e.ClickedItem;
            this.Frame.Navigate(typeof(DetailView), e.ClickedItem);
        }
    }
}
