using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.Utilities;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ePs.PatientLive.Framework.DataModel;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class MyStudiesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<StudyTile> _items;
        public ObservableCollection<StudyTile> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Items"));
                }
            }
        }
        public User CurrentUser { get; set; }
    
        public MyStudiesModel()
        {
            var user = SuspensionManager.SessionState["CurrentUser"] as User;
            
            LoadItems(user);
        }

        public async void LoadItems(User user)
        {
            CurrentUser = await DataService.GetUser(user.WindowsLiveID);
            var items = await DataService.GetMyStudyTiles(CurrentUser.user_id);
            Items = new ObservableCollection<StudyTile>(items);
        }
    }
}
