using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ePs.PatientLive.Framework.DataModel;
using System.Collections.Specialized;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Runtime.Serialization;
using System.IO;
using Windows.Storage.Streams;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class ResearchStudiesViewModel : BindableBase
    {
        private CoreDispatcher _viewUI;
        private GridView _resultsGridView;
        private ProgressBar _progressStatus;
        private string _postalCode = null;
        public ObservableCollection<StudyTile> StudyTiles { get; private set; }
        public User CurrentUser { get; private set; }
        public string SelectedConditions { get; set; }
        private Gradient NewTileBg { get; set; }
        private Gradient StudyTileBg { get; set; }
        public string PostalCode 
        {
            get
            {
                if (_postalCode != null) return _postalCode;

                return CurrentUser.zip_code;
            }

            set
            {
                _postalCode = value;
            }
        }

        public string Gender { get; set; }

        public ResearchStudiesViewModel(CoreDispatcher viewUI, GridView resultsGridView, ProgressBar progressStatus)
        {
            _viewUI            = viewUI;
            _resultsGridView   = resultsGridView;
            _progressStatus    = progressStatus;
            CurrentUser        = SuspensionManager.SessionState["CurrentUser"] as User;
            SelectedConditions = TypeAheadHelper.GetConditionString(CurrentUser);
            
            DataService.CollectionChanged += StudyCollectionChanged;

            StudyTiles = new ObservableCollection<StudyTile>();

            StudyTileBg = new Gradient
            {
                Start = HexToColorConverter.ConvertFromHex("#FF7FA3CF"),
                End = HexToColorConverter.ConvertFromHex("#FF3E80CF")
            };

            NewTileBg = new Gradient
            {
                Start = HexToColorConverter.ConvertFromHex("#FF57d763"),
                End = HexToColorConverter.ConvertFromHex("#FF3ea747")
            };
        }

        public void GetResearchStudies(int age, string gender, string conditions, string postalCode, string empty, int distance, decimal latitude, decimal longitude, int skip, int take, string dob)
        {
            if (StudyTiles != null) StudyTiles.Clear();

            DataService.CreateUserSearchLog(CurrentUser, age, gender, conditions, string.Empty, postalCode, distance);
            DataService.GetResearchStudies(age, gender, conditions, postalCode, empty, distance, latitude, longitude, 0, 50, CurrentUser.user_id, dob);
        }

        private void StudyCollectionChanged(EventArgs e)
        {
            string MyClinicalStudyPath = "http://prod11.epharmasolutions.com/survey/TakeSurvey.aspx?SurveyID=";
            if (DataService.StudyResults != null)
            {
                var studyTiles = from c in DataService.StudyResults
                                 select new StudyTile
                                 {
                                     Title               = c.Title,
                                     Condition           = c.Conditions,
                                     Eligibility         = c.EligibilityCriteriaTextblock,
                                     Gender              = c.EligibilityGender,
                                     HealthyVolunteersYN = c.EligibilityHealthyVolunteersYN,
                                     MaxAge              = c.EligibilityMaxAgeYears,
                                     MinAge              = c.EligibilityMinAgeYears,
                                     Enrollment          = c.Enrollment,
                                     Phase               = c.Phase,
                                     SiteBackupContact   = c.SiteBackupContact,
                                     SitePrimaryContact  = c.SitePrimaryContact,
                                     Sponsor             = c.Sponsor,
                                     StudyBackupContact  = c.StudyBackupContact,
                                     StudyPrimaryContact = c.StudyPrimaryContact,
                                     StudyEndDate        = c.StudyEndDate,
                                     Status              = c.StudyStatus,
                                     StudyType           = c.StudyType,
                                     SurveyId            = c.SurveyId,
                                     Distance            = c.Distance,
                                     City                = c.City,
                                     Background          = c.IsNew == 1 ? NewTileBg : StudyTileBg,
                                     StudyId             = c.StudyId ?? 0,
                                     SiteId              = c.SiteId,
                                     URL                 = c.SurveyId != null && c.SurveyId != 0 ? MyClinicalStudyPath + c.SurveyId.ToString() : string.Empty
                                 };

                StudyTiles = new ObservableCollection<StudyTile>(studyTiles);
                Task.Run(() => UpdateResultsGridView());

                try
                {
                    SaveStudyResults(StudyTiles);
                }
                catch (Exception) 
                { }
            }
        }

        private async void SaveStudyResults(ObservableCollection<StudyTile> StudyTiles)
        {
            StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync("SearchResults", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);

            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<StudyTile>));
                serializer.WriteObject(outStream.AsStreamForWrite(), StudyTiles);
                await outStream.FlushAsync();
            }
            raStream.Dispose();
        }

        public async void SetStudyResults()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("SearchResults");
            if (file == null) return;

            IRandomAccessStream inStream = await file.OpenReadAsync();

            DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<StudyTile>));

            StudyTiles = (ObservableCollection<StudyTile>)serializer.ReadObject(inStream.AsStreamForRead());
            inStream.Dispose();
            await Task.Run(() => UpdateResultsGridView());           
        }

        private async Task UpdateResultsGridView()
        {
            await _viewUI.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _resultsGridView.ItemsSource = StudyTiles;
                _progressStatus.Visibility      = Visibility.Collapsed;
            });
        }
    }
}