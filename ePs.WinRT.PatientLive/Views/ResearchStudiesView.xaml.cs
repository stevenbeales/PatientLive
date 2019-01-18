using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.ViewModels;
using ePs.PatientLive.Framework.Utilities;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ePs.PatientLive.Framework.DataModel;
using System.ComponentModel;
using Windows.UI.Core;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using Windows.System;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.Runtime.Serialization;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ResearchStudiesView : LayoutAwarePage
    {
        public List<string> SelectedConditions { get; set; }
        private ResearchStudiesViewModel _viewModel { get; set; }
        
        public ResearchStudiesView()
        {
            this.InitializeComponent();

            _viewModel                = new ResearchStudiesViewModel(Window.Current.Dispatcher, itemGridView, ProgressStatus);
            DataContext               = _viewModel;
            //PostalCodeTxt.Text        = _viewModel.CurrentUser.zip_code;
            //GenderCombo.SelectedValue = _viewModel.CurrentUser.Gender ?? string.Empty;
            _viewModel.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

            // If there are new studies initiate the search
            var studyCount = Int32.Parse(SuspensionManager.SessionState["StudyCount"].ToString());
            if (studyCount > 0)
            {
                Search();
            }

            // Set a default zip and lat/long in session
            if (!SuspensionManager.SessionState.ContainsKey("PostCode"))
            {
                SetLocation(_viewModel.CurrentUser);
            }

            if (SuspensionManager.SessionState.ContainsKey("SearchParams"))
            {
                SetSearchParams(SuspensionManager.SessionState["SearchParams"] as Dictionary<string, string>);
            }
        }

        private void SetSearchParams(Dictionary<string, string> dict)
        {
            var distance = Int32.Parse(dict["distance"]);
            
            ConditionText.Text = dict["conditions"];
            AgeText.Text = dict["age"];
            GenderCombo.SelectedValue = dict["gender"];
            PostalCodeText.Text = dict["postalCode"];
            DistanceCombo.SelectedValue = distance == 1000000 ? "Any" : distance.ToString();

            _viewModel.SetStudyResults();
        }
 
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != null)
            {
                switch (e.PropertyName)
                {
                    case "":
                        break;
                    default:
                        break;
                }
            }
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

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            StudyTile tile = (StudyTile)e.ClickedItem;
            this.Frame.Navigate(typeof(Screener), e.ClickedItem);
        }

        private void ItemView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems   = itemGridView.SelectedItems;
            bottomAppBar.IsOpen = selectedItems.Count > 0;
        }

        private void ConditionText_LostFocus(object sender, RoutedEventArgs e)
        {
            ConditionListBox.Visibility = Visibility.Collapsed;
        }

        private void ConditionText_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionText.Text.Trim()), ConditionListBox, "conditions");
            ConditionText.Select(ConditionText.Text.Length, 0);
        }

        private void ConditionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConditionText.Text.Length <= 0)
                _viewModel.SelectedConditions = string.Empty;
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionText.Text.Trim()), ConditionListBox, "conditions");
        }
        
        private void ConditionText_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionText.Text.Trim()), ConditionListBox, "conditions");
        }

        private void ConditionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = TypeAheadHelper.SelectionChanged(ConditionListBox, _viewModel.SelectedConditions, ConditionText);

            if (items.Length > 0)
                _viewModel.SelectedConditions = items;
        }

        private async void SetSelectBox(string text, ListBox listBox, string type)
        {
            if (text.Length > 0)
            {
                var items           = await Task.Run(() => TypeAheadHelper.GetResults(text, type));
                listBox.ItemsSource = items;
                listBox.Visibility  = Visibility.Visible;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = itemGridView.SelectedItems;
            if (selectedItems.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (var i in selectedItems)
                {
                    var tile = i as StudyTile;
                    var ids = tile.StudyId + "," + tile.SiteId;
                    list.Add(ids);
                }

                DataService.SaveStudies(_viewModel.CurrentUser, list);
            }

            bottomAppBar.IsOpen = false;
        }

        private async void GetPostalCode(object sender, TappedRoutedEventArgs e)
        {
            var geo                    = new Geolocator();
            Geoposition pos            = await geo.GetGeopositionAsync().AsTask();
            PostalCodeImg.IsTapEnabled = false;
            var postCode               = string.Empty;

            try 
            {
                PostCodeRing.IsActive = true;                
                postCode              = await Task<string>.Run(() => GeocodeServiceHelper.GetPostalCode(pos));

                if(postCode.Length <= 0)
                    PCNotFoundTxt.Visibility = Visibility.Visible;
                else
                    PostalCodeText.Text = postCode;

                PostCodeRing.IsActive = false;
            }
            catch (Exception)
            {
                PostCodeRing.IsActive = false;
            }
            PostalCodeImg.IsTapEnabled = true;
        }

        private void PostalCodeText_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            PCNotFoundTxt.Visibility = Visibility.Collapsed;
        }

        private void SearchBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Search();
        }
        
        private async void Search()
        {
            decimal latitude  = 0;
            decimal longitude = 0;
            var emptyString   = "\"\"";
            var age           = AgeText.Text.Length > 0 ? Int32.Parse(AgeText.Text) : -1;
            var gender        = GenderCombo.SelectedValue != null ? GenderCombo.SelectedValue.ToString() : emptyString;
            var conditions    = ConditionText.Text.Length > 0 ? ConditionText.Text : emptyString;
            var postalCode    = PostalCodeText.Text.Length > 0 ? PostalCodeText.Text : emptyString;
            var distance      = DistanceCombo.SelectedValue != null ? (DistanceCombo.SelectedValue.ToString() == "Any" ? 1000000 : Int32.Parse(DistanceCombo.SelectedValue.ToString())) : 1000000;
            var postCode      = PostalCodeText.Text;

            if (postCode.Length > 0 && postCode != SuspensionManager.SessionState["PostCode"].ToString())
            {
                try
                {
                    var response = await Task<string>.Run(() => GeocodeServiceHelper.GetGeocodeByAddress(postCode));
                    if (response.Results != null)
                    {
                        latitude = Convert.ToDecimal(response.Results[0].Locations[0].Latitude);
                        longitude = Convert.ToDecimal(response.Results[0].Locations[0].Longitude);
                    }
                }
                catch (Exception)
                {
                    // Swallow exception
                }
            }
            else
            {
                latitude = Convert.ToDecimal(SuspensionManager.SessionState["Latitude"].ToString());
                longitude = Convert.ToDecimal(SuspensionManager.SessionState["Longitude"].ToString());
            }

            var paramSet = new Dictionary<string, string>();
            paramSet.Add("age", age.ToString());
            paramSet.Add("gender", gender);
            paramSet.Add("conditions", conditions);
            paramSet.Add("postalCode", postalCode);
            paramSet.Add("distance", distance.ToString());
            SetSessionVariable("SearchParams", paramSet);

            ProgressStatus.Visibility = Visibility.Visible;
            _viewModel.GetResearchStudies(age, gender, conditions, postalCode, emptyString, distance, latitude, longitude, 0, 50, "1900-01-01");
        }

        private void SetSessionVariable(string key, object obj)
        {
            if (SuspensionManager.SessionState.ContainsKey(key))
                SuspensionManager.SessionState[key] = obj;
            else
                SuspensionManager.SessionState.Add(key, obj);
        }

        private void ClearBtn_Clicked(object sender, RoutedEventArgs e)
        {
            ConditionText.Text           = string.Empty;
            AgeText.Text                    = string.Empty;
            PostalCodeText.Text          = string.Empty;
            DistanceCombo.SelectedValue = string.Empty;
        }

        private async void SetLocation(User user)
        {
            decimal latitude = 0;
            decimal longitude = 0;
            var postCode = string.Empty;
            SuspensionManager.SessionState.Add("PostCode", postCode);
            SuspensionManager.SessionState.Add("Latitude", latitude);
            SuspensionManager.SessionState.Add("Longitude", longitude);

            if (user != null && user.zip_code != null)
                postCode = user.zip_code;
            else
            {
                var geo = new Geolocator();
                Geoposition pos = await geo.GetGeopositionAsync().AsTask();
                postCode = await Task<string>.Run(() => GeocodeServiceHelper.GetPostalCode(pos));
            }

            if (postCode.Length > 0)
            {
                SuspensionManager.SessionState["PostCode"] = postCode;
                var response = await Task<string>.Run(() => GeocodeServiceHelper.GetGeocodeByAddress(postCode));
                if (response.Results != null)
                {
                    latitude = Convert.ToDecimal(response.Results[0].Locations[0].Latitude);
                    longitude = Convert.ToDecimal(response.Results[0].Locations[0].Longitude);
                    SuspensionManager.SessionState["Latitude"] = latitude;
                    SuspensionManager.SessionState["Longitude"] = longitude;
                }
            }
        }

        private void AgeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = AgeText.Text;
            if (text.Length > 0)
            {
                char last = text[text.Length - 1];
                if (!Char.IsDigit(last))
                {
                    AgeText.Text = text.Substring(0, text.Length - 1);
                }
            }
        }
    }
}
