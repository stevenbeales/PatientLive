using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ePs.PatientLive.Framework.Utilities;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.DataModel;
using ePs.WinRt.PatientLive.Views;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace ePs.WinRT.PatientLive.Views
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class SearchMedications : LayoutAwarePage
    {
        public List<string> SelectedConditions { get; set; }
        public SearchMedicationsModel ViewModel { get; set; }

        public SearchMedications()
        {
            this.InitializeComponent();

            ViewModel = new SearchMedicationsModel();
            this.DataContext = ViewModel;

            SetParams();
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

        private void SetParams()
        {
            if(SuspensionManager.SessionState.ContainsKey("SearchMedParams") && SuspensionManager.SessionState["SearchMedParams"] != null)
            {
                var par = SuspensionManager.SessionState["SearchMedParams"].ToString().Split(':');
                if (par.Count() == 2)
                {
                    ConditionTxt.Text = par[0];
                    MedicationTxt.Text = par[1];
                    //SuspensionManager.SessionState["SearchMedParams"] = null;
                    //Search();
                    SetMedResults();
                }
            }
            else if (Int32.Parse(SuspensionManager.SessionState["MedCount"].ToString()) > 0)
            {
                ConditionTxt.Text = TypeAheadHelper.GetConditionString(SuspensionManager.SessionState["UserConditions"] as List<Condition>);
                MedicationTxt.Text = TypeAheadHelper.GetMedicationString(SuspensionManager.SessionState["UserMedications"] as List<Medication>);
                Search();
            }
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            MedTile tile = (MedTile)e.ClickedItem;
            //Launcher.LaunchUriAsync(new Uri(tile.Url));           
            this.Frame.Navigate(typeof(DetailView), e.ClickedItem);
        }

        private void ClearBtn_Clicked(object sender, RoutedEventArgs e)
        {
            ConditionTxt.Text = string.Empty;
            ViewModel.SelectedConditions = string.Empty;

            MedicationTxt.Text = string.Empty;
            ViewModel.SelectedMedications = string.Empty;
        }

        private void SearchBtn_Clicked(object sender, RoutedEventArgs e)
        {
            SuspensionManager.SessionState["SearchMedParams"] = ConditionTxt.Text + ":" + MedicationTxt.Text;
            
            Search();
        }

        private void Search()
        {
            ViewModel.Items = new MedTileCollection
            {
                Conditions = ConditionTxt.Text,
                Medications = MedicationTxt.Text,
                UserId = ViewModel.CurrentUser.user_id
            };
            ViewModel.Items.LoadingStarted += Items_LoadingStarted;
            ViewModel.Items.LoadingCompleted += Items_LoadingCompleted;
            itemGridView.ItemsSource = ViewModel.Items;

            DataService.CreateUserSearchLog(ViewModel.CurrentUser, null, ViewModel.CurrentUser.Gender, ConditionTxt.Text,
                MedicationTxt.Text, ViewModel.CurrentUser.zip_code ?? string.Empty, null);

        }

        private void Items_LoadingStarted(object sender, IncrementalLoadingStartedEventArgs e)
        {
            ProgressStatus.Visibility = Visibility.Visible;
        }

        private void Items_LoadingCompleted(object sender, IncrementalLoadingCompletedEventArgs e)
        {
            ProgressStatus.Visibility = Visibility.Collapsed;
            itemGridView.ItemsSource = ViewModel.Items;
            SaveMedResults(ViewModel.Items);
        }

        private async void SaveMedResults(MedTileCollection MedTiles)
        {
            StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync("SearchMedResults", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);

            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(MedTileCollection));
                serializer.WriteObject(outStream.AsStreamForWrite(), MedTiles);
                await outStream.FlushAsync();
            }
            raStream.Dispose();
        }

        public async void SetMedResults()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("SearchMedResults");
            if (file == null) return;

            IRandomAccessStream inStream = await file.OpenReadAsync();

            DataContractSerializer serializer = new DataContractSerializer(typeof(MedTileCollection));

            ViewModel.Items = (MedTileCollection)serializer.ReadObject(inStream.AsStreamForRead());
            inStream.Dispose();
            itemGridView.ItemsSource = ViewModel.Items;
        }

        private async void SetSelectBox(string text, ListBox listBox, string type)
        {
            if (text.Length > 0)
            {
                //ring.Visibility = Visibility.Visible;
                var items = await Task.Run(() => ViewModel.GetResults(text, type));
                listBox.ItemsSource = items;
                //ring.Visibility = Visibility.Collapsed;
                listBox.Visibility = Visibility.Visible;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
                //ViewModel.ClearSelected(type);
            }
        }

        private void ConditionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConditionTxt.Text.Length <= 0)
                ViewModel.SelectedConditions = string.Empty;
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionTxt.Text.Trim()), ConditionSelect, "conditions");
        }

        private void ConditionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ViewModel.SelectedCondtions = TypeAheadHelper.UpdateSelected(ViewModel.SelectedConditions, ConditionTxt);
            var items = TypeAheadHelper.SelectionChanged(ConditionSelect, ViewModel.SelectedConditions, ConditionTxt);
            if (items.Length > 0)
                ViewModel.SelectedConditions = items;
        }

        private void MedicationTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MedicationTxt.Text.Length <= 0)
                ViewModel.SelectedMedications = string.Empty;
            SetSelectBox(TypeAheadHelper.GetSearchString(MedicationTxt.Text.Trim()), MedicationSelect, "medications");
        }
        

        private void MedicationSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = TypeAheadHelper.SelectionChanged(MedicationSelect, ViewModel.SelectedMedications, MedicationTxt);
            if (items.Length > 0)
                ViewModel.SelectedConditions = items;

        }

        private void ConditionTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            ConditionSelect.Visibility = Visibility.Collapsed;
        }

        private void ConditionTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(ConditionTxt.Text.Trim()), ConditionSelect, "conditions");
            ConditionTxt.Select(ConditionTxt.Text.Length, 0);
        }

        private void MedicationTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SetSelectBox(TypeAheadHelper.GetSearchString(MedicationTxt.Text.Trim()), MedicationSelect, "medications");
            MedicationTxt.Select(MedicationTxt.Text.Length, 0);
        }

        private void MedicationTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            MedicationSelect.Visibility = Visibility.Collapsed;
        }
        
    }
}
