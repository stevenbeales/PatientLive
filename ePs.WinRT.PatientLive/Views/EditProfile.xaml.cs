using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.ViewModels;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.GeocodeService;
using Microsoft.Live;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using ePs.PatientLive.Framework.DataModel;
using System.ComponentModel;
using Windows.System.Threading;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EditProfile : LayoutAwarePage
    {
        public List<string> SelectedConditions { get; set; }
        public ProfileModel ViewModel { get; set; }
        
        public EditProfile()
        {
            this.InitializeComponent(); 
            ViewModel = new ProfileModel();
            ViewModel.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            this.DataContext = ViewModel;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser" && ViewModel.CurrentUser != null)
            {
                SuspensionManager.SessionState["CurrentUser"] = ViewModel.CurrentUser;
                Save();
            }
            else if(e.PropertyName == "Medications")
                Redirect();
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
            Load();
        }

        private async void Load()
        {
            await PopulateDropDowns();
            await SetFields();

            if (SuspensionManager.SessionState.ContainsKey("StudyTile") && SuspensionManager.SessionState["StudyTile"] != null)
            {
                var md = new MessageDialog("Before we proceed, please complete/confirm the information in your profile and click save.");
                
                await md.ShowAsync();
            }
        }

        private async Task SetFields()
        {
            var currentUser = ViewModel.CurrentUser;
            if (currentUser != null && currentUser.email_address != null)
            {
                FirstNameTxt.Text          = currentUser.first_name ?? string.Empty;
                LastNameTxt.Text           = currentUser.last_name ?? string.Empty;
                AddressTxt.Text            = currentUser.address_1 ?? string.Empty;
                CityTxt.Text               = currentUser.city ?? string.Empty;
                PostalCodeTxt.Text         = currentUser.zip_code ?? string.Empty;
                CountryCombo.SelectedValue = currentUser.country ?? string.Empty;
                StateCombo.SelectedValue   = currentUser.state ?? string.Empty;
                GenderCombo.SelectedValue  = ViewModel.GenderString;
                EmailTxt.Text              = currentUser.email_address ?? string.Empty;
                MobileNumber.Text          = currentUser.mobile_phone ?? string.Empty;
                
                if (currentUser.DOB != null)
                {
                    DayList.SelectedValue   = currentUser.DOB.Value.Day;
                    MonthList.SelectedValue = currentUser.DOB.Value.Month;
                    YearList.SelectedValue  = currentUser.DOB.Value.Year;
                }
            }
            else
            {
                LiveConnectClient liveClient        = new LiveConnectClient(SuspensionManager.SessionState["LiveConnectSession"] as LiveConnectSession);
                LiveOperationResult operationResult = await liveClient.GetAsync("me");
                dynamic result                      = operationResult.Result;
                FirstNameTxt.Text                   = operationResult.Result["first_name"] == null ? string.Empty : operationResult.Result["first_name"].ToString();
                LastNameTxt.Text                    = operationResult.Result["last_name"] == null ? string.Empty : operationResult.Result["last_name"].ToString();
                GenderCombo.SelectedValue           = operationResult.Result["gender"] == null ? string.Empty : operationResult.Result["gender"].ToString();
            }
        }

        private async Task PopulateDropDowns()
        {
            for (int i = 1; i < 32; i++)
            {
                if (i < 13)
                    ViewModel.MonthList.Add(i);
                ViewModel.DayList.Add(i);
            }

            var year = DateTime.Now.Year;
            for (int i = year; i > year - 100; i--)
            {
                ViewModel.YearList.Add(i);
            }

            DayList.ItemsSource = ViewModel.DayList.ToList();
            MonthList.ItemsSource = ViewModel.MonthList.ToList();
            YearList.ItemsSource = ViewModel.YearList.ToList();

            StateCombo.ItemsSource = await Task.Run(() => ViewModel.GetStateList());
            CountryCombo.ItemsSource = await Task.Run(() => ViewModel.GetCountryList());
        }

        

        private void ConditionTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            ConditionSelect.Visibility = Visibility.Collapsed;
        }

        private void ConditionTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionTxt.Text.Trim()), ConditionSelect, "conditions", ConditionRing);
            ConditionTxt.Select(ConditionTxt.Text.Length, 0);
        }

        private void MedicationTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(MedicationTxt.Text.Trim()), MedicationSelect, "medications", MedicationRing);
            MedicationTxt.Select(MedicationTxt.Text.Length, 0);
        }

        private void MedicationTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            MedicationSelect.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SetSelectBox(string text, ListBox listBox, string type, ProgressRing ring)
        {
            if (text.Length > 0)
            {
                ring.Visibility = Visibility.Visible;
                var items = await Task.Run(() => ViewModel.GetResults(text, type));
                listBox.ItemsSource = items;
                ring.Visibility = Visibility.Collapsed;
                listBox.Visibility = Visibility.Visible;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
            }
        }

        private void ConditionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConditionTxt.Text.Length <= 0)
                ViewModel.SelectedConditions = string.Empty;
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionTxt.Text.Trim()), ConditionSelect, "conditions", ConditionRing);
        }
        
        private void ConditionTxt_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionTxt.Text.Trim()), ConditionSelect, "conditions", ConditionRing);
        }

        private void ConditionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = TypeAheadHelper.SelectionChanged(ConditionSelect, ConditionTxt.Text, ConditionTxt);
            if (items.Length > 0)
                ViewModel.SelectedConditions = items;
        }

        private void MedicationTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MedicationTxt.Text.Length <= 0)
                ViewModel.SelectedMedications = string.Empty;
            SetSelectBox(TypeAheadHelper.GetSearchString(MedicationTxt.Text.Trim()), MedicationSelect, "medications", MedicationRing);
        }
        
        private void MedicationTxt_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(MedicationTxt.Text.Trim()), MedicationSelect, "medications", MedicationRing);
        }

        private void MedicationSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = TypeAheadHelper.SelectionChanged(MedicationSelect, MedicationTxt.Text, MedicationTxt);
            if (items.Length > 0)
                ViewModel.SelectedMedications = items;
        }

        private async void GetLocation(object sender, TappedRoutedEventArgs e)
        {
            PostalCodeImg.IsTapEnabled = false;
            CityImg.IsTapEnabled = false;
            CountryImg.IsTapEnabled = false;
            var activeRing = PostCodeRing;
            try
            {
                Image clicked = (Image)sender;
                
                switch (clicked.Name)
                { 
                    case "PostalCodeImg":
                        PostCodeRing.IsActive = true;
                        break;
                    case "CityImg":
                        CityRing.IsActive = true;
                        activeRing = CityRing;
                        break;
                    case "CountryImg":
                        CountryRing.IsActive = true;
                        activeRing = CountryRing;
                        break;
                }

                var geo = new Geolocator();
                Geoposition pos = await geo.GetGeopositionAsync().AsTask();
                var location = await Task<GeocodeResponse>.Run(() => GeocodeServiceHelper.GetLocation(pos));
                if (location.Results != null)
                {
                    PostalCodeTxt.Text = location.Results[0].Address.PostalCode;
                    CityTxt.Text = location.Results[0].Address.Locality;
                    CountryCombo.SelectedValue = location.Results[0].Address.CountryRegion;
                    StateCombo.SelectedValue = location.Results[0].Address.AdminDistrict;
                }
                activeRing.IsActive = false;
            }
            catch (Exception)
            {
                activeRing.IsActive = false;
                PostalCodeImg.IsTapEnabled = true;
                CityImg.IsTapEnabled = true;
                CountryImg.IsTapEnabled = true;
            }
            PostalCodeImg.IsTapEnabled = true;
            CityImg.IsTapEnabled = true;
            CountryImg.IsTapEnabled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshCurrentUser(ViewModel.CurrentUser.WindowsLiveID.ToString());
        }
        
        private async void Save()
        {           
            ViewModel.SavingData = true;
            if ((EmailTxt.Text == null || EmailTxt.Text.Length <= 0) && IsValid(EmailTxt.Text))
            {
                var md = new MessageDialog("A valid email address is required.");
                await md.ShowAsync();
                return;
            }

            var year = YearList.SelectedValue != null ? YearList.SelectedValue.ToString() : string.Empty;

            DateTime birthDate;
            if (year.Length > 0 || DayList.SelectedValue != null || MonthList.SelectedValue != null)
            {
                var day = DayList.SelectedValue != null ? DayList.SelectedValue.ToString() : "1";
                var month = MonthList.SelectedValue != null ? MonthList.SelectedValue.ToString() : "1";
                var dateString = month + "/" + day + "/" + year;
                if (DateTime.TryParse(dateString, out birthDate) == false)
                {
                    var md = new MessageDialog("The birth date is invalid");
                    await md.ShowAsync();
                    return;
                }

                ViewModel.CurrentUser.DOB = birthDate;
            }
            
            var address = AddressTxt.Text + " " + PostalCodeTxt.Text;
            
            if (address.Length > 0)
            {
                try
                {
                    var location = await Task<GeocodeResponse>.Run(() => GeocodeServiceHelper.GetGeocodeByAddress(address));
                    if (location.Results != null)
                    {
                        var lat = location.Results[0].Locations[0].Latitude;
                        var lon = location.Results[0].Locations[0].Longitude;
                    }
                }
                catch (Exception)
                { 
                    
                }
            }
                       
            ViewModel.CurrentUser.first_name = FirstNameTxt.Text;
            ViewModel.CurrentUser.last_name = LastNameTxt.Text;
            ViewModel.CurrentUser.address_1 = AddressTxt.Text;
            ViewModel.CurrentUser.city = CityTxt.Text;
            ViewModel.CurrentUser.zip_code = PostalCodeTxt.Text;
            ViewModel.CurrentUser.country = CountryCombo.SelectedValue != null ? CountryCombo.SelectedValue.ToString() : string.Empty;
            ViewModel.CurrentUser.email_address = EmailTxt.Text;
            ViewModel.CurrentUser.state = StateCombo.SelectedValue != null ? StateCombo.SelectedValue.ToString() : string.Empty;
            ViewModel.CurrentUser.mobile_phone = MobileNumber.Text;
            ViewModel.CurrentUser.Gender = GenderCombo.SelectedValue != null ? GenderCombo.SelectedValue.ToString() == "Male" ? "M" : "F" : string.Empty;

            await ViewModel.SaveUser(ViewModel.CurrentUser, ConditionTxt.Text, MedicationTxt.Text);

            SuspensionManager.SessionState["CurrentUser"] = ViewModel.CurrentUser; 
        }

        public async void Redirect()
        {
            if (ViewModel.SavingData)
            {
                ViewModel.SavingData = false;
                //If there's a study tile in session send them back to the study screener
                if (SuspensionManager.SessionState.ContainsKey("StudyTile") && SuspensionManager.SessionState["StudyTile"] != null)
                {
                    // If they didn't add any conditions warn them first
                    if (ViewModel.CurrentUser.UserConditions.Where(o => o.Deleted == null).Count() <= 0)
                    {
                        var md = new MessageDialog("Profile not complete (no conditions specified); Proceed anyway?");
                        md.Commands.Add(new UICommand("Ok", (UICommandInvokedHandler) =>
                        {
                            Back();
                        }));
                        md.Commands.Add(new UICommand("Cancel"));

                        await md.ShowAsync();
                    }
                    else
                    {
                        Back();
                    }
                }
                else
                    this.Frame.GoBack();
            }
        }

        private void Back()
        {
            SuspensionManager.SessionState["StudyTile"] = null;

            if (!SuspensionManager.SessionState.ContainsKey("ShowContactInfo"))
                SuspensionManager.SessionState.Add("ShowContactInfo", true);
            else
                SuspensionManager.SessionState["ShowContactInfo"] = true;
            this.Frame.GoBack();
        }

        public bool IsValid(string email)
        {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return regex.IsMatch(email);

        }

    }
}
