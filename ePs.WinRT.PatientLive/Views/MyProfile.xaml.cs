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
using ePs.PatientLive.Framework.ViewModels;
using ePs.PatientLive.Framework.DataModel;
using System.ComponentModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MyProfile : LayoutAwarePage
    {
        public ProfileModel ViewModel { get; set; }
        //private ObservableProfileModel ViewModel { get; set; }

        public MyProfile()
        {
            this.InitializeComponent();
            ViewModel = new ProfileModel();
            ViewModel.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            this.DataContext = ViewModel;
            ConditionsList.ItemsSource = ViewModel.Conditions;
            MedicationsList.ItemsSource = ViewModel.Medications;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //SetFields();
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
            if (ViewModel.CurrentUser.email_address == null || ViewModel.CurrentUser.email_address.Length <= 0)
                this.Frame.Navigate(typeof(EditProfile));
        }

        private async void Load()
        {
            ViewModel.CurrentUser = await DataService.GetUser(ViewModel.CurrentUser.WindowsLiveID);
            SetFields();
        }

        private void SetFields()
        {
            ConditionsList.ItemsSource = ViewModel.CurrentUser.UserConditions.Select(o => o.Condition.Name);
            MedicationsList.ItemsSource = ViewModel.CurrentUser.UserMedications.Select(o => o.Medication.Name);
            NameTxt.Text = ViewModel.FullName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditProfile));
        }
    }
}
