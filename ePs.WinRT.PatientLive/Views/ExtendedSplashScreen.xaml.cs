using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.MyClinicalStudyService;

using ePs.WinRT.PatientLive.Views;
using Microsoft.Live;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ePs.PatientLive.Framework.Utilities;
using ePs.WinRT.PatientLive.Views.Shared;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ePs.WinRt.PatientLive.Views
{
    public class ExtendedSplashScreenModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User _currentUser = new User();
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentUser"));

            }
        }

        private int _alertCount;
        public int AlertCount
        {
            get { return _alertCount; }
            set
            {
                _alertCount = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("AlertCount"));
            }
        }

        private int _medCount;
        public int MedCount
        {
            get { return _medCount; }
            set
            {
                _medCount = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MedCount"));
            }
        }

        private int _studyCount;
        public int StudyCount
        {
            get { return _studyCount; }
            set
            {
                _studyCount = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StudyCount"));
            }
        }

        public ExtendedSplashScreenModel()
        {
            DataService.CollectionChanged += ItemCountCollectionChanged;
        }

        private void ItemCountCollectionChanged(EventArgs e)
        {
            if (DataService.ItemCountResults != null)
            {
                var results = DataService.ItemCountResults.ToList();
                AlertCount = results.Where(o => o.Item == "NewAlerts").Select(o => o.Count).FirstOrDefault() ?? 0;
                MedCount = results.Where(o => o.Item == "NewMedications").Select(o => o.Count).FirstOrDefault() ?? 0;
                StudyCount = results.Where(o => o.Item == "NewStudies").Select(o => o.Count).FirstOrDefault() ?? 0;
            }
        }

    }
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplashScreen : Page
    {
        public ExtendedSplashScreenModel Model;

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser")
            {
                SuspensionManager.SessionState["CurrentUser"] = Model.CurrentUser;
                SuspensionManager.SessionState.Add("UserConditions", Model.CurrentUser.UserConditions.Where(o => o.Deleted == null).Select(o => o.Condition).ToList());
                SuspensionManager.SessionState.Add("UserMedications", Model.CurrentUser.UserMedications.Where(o => o.Deleted == null).Select(o => o.Medication).ToList());
                DataService.GetNewItemCount(Model.CurrentUser.user_id);
            }
            if (e.PropertyName == "StudyCount")
            {
                SuspensionManager.SessionState.Add("AlertCount", Model.AlertCount);
                SuspensionManager.SessionState.Add("MedCount", Model.MedCount);
                SuspensionManager.SessionState.Add("StudyCount", Model.StudyCount);

                this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    this.Frame.Navigate(typeof(MasterPage));
                });

            }
            
        }

        public ExtendedSplashScreen()
        {
            this.InitializeComponent();
            Model = new ExtendedSplashScreenModel();
            Model.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            SuspensionManager.SessionState.Add("CurrentUser", new User());
            InitAuth();
            DeleteStudyResults();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void DeleteStudyResults()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("SearchResults");
                if (file == null) return;

                await file.DeleteAsync();
            }
            catch (Exception)
            { 
                // apparently there is no way to check if the file exists.. :\
            }
        }

        internal async void InitAuth()
        {
                   
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var authClient = new LiveAuthClient();

                LiveLoginResult authResult;
                try
                {
                    authResult = await authClient.LoginAsync(new List<string>() { "wl.signin", "wl.basic", "wl.emails" });

                    if (authResult.Status == LiveConnectSessionStatus.Connected)
                    {

                        SuspensionManager.SessionState.Add("LiveConnectSession", authResult.Session);
                        LiveConnectClient liveClient = new LiveConnectClient(authResult.Session);
                        LiveOperationResult operationResult = await liveClient.GetAsync("me");
                        var liveId = operationResult.Result["id"].ToString();
                        var firstName = operationResult.Result["first_name"] == null ? string.Empty : operationResult.Result["first_name"].ToString();
                        var lastName = operationResult.Result["last_name"] == null ? string.Empty : operationResult.Result["last_name"].ToString();
                        Dictionary<string, string> d = operationResult.Result["emails"] as Dictionary<string, string>;
                        var emails = (IDictionary<string, object>)operationResult.Result["emails"];
                        var email = emails["preferred"] != null ? emails["preferred"].ToString() :
                                    emails["account"] != null ? emails["account"].ToString() :
                                    emails["personal"] != null ? emails["personal"].ToString() :
                                    emails["business"] != null ? emails["business"].ToString() :
                                    string.Empty;

                        var user = await DataService.GetUser(liveId);
                        
                        if (user == null)
                        {
                            user = DataService.CreateUser(liveId, firstName, lastName, email);
                        }

                        Model.CurrentUser = user;
                    }
                    else
                    {
                        var md = new MessageDialog("A Windows Live Id is required. Click Ok to verify your Windows Live account.");
                        md.Commands.Add(new UICommand("Ok", (UICommandInvokedHandler) =>
                        {
                            InitAuth();
                        }));
                        
                        await md.ShowAsync();
                    }
                }
                catch (Exception ex)
                {
                    var md = new MessageDialog("There was an error accessing the network: " + ex.Message + " Click ok to try again.");
                    md.Commands.Add(new UICommand("Ok", (UICommandInvokedHandler) =>
                    {
                        InitAuth();
                    }));
                    md.ShowAsync(); 
                }

            }

        }
    }
}
