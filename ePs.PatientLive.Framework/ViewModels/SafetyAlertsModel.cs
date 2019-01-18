using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class SafetyAlertsModel : INotifyPropertyChanged
    {
        private ObservableCollection<SafetyTile> _items;
        public ObservableCollection<SafetyTile> Items
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

        public SafetyAlertsModel()
        {
            CurrentUser = SuspensionManager.SessionState["CurrentUser"] as User;
            LoadItems();
        }

        public async void LoadItems()
        {
            var items = await DataService.GetSafetyAlerts(CurrentUser.user_id);
            Items = new ObservableCollection<SafetyTile>(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
