using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class ComboBoxItemsModel : INotifyPropertyChanged
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }

        public string Text { get; set; }


        #region " INotifyPropertyChanged Interface "

        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
