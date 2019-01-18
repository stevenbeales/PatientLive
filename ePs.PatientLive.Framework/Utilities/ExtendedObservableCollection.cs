using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.Utilities
{
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suspendCollectionChangeNotification;

        public ExtendedObservableCollection()
            : base()
        {
            _suspendCollectionChangeNotification = false;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suspendCollectionChangeNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void SuspendCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = true;
        }

        public void ResumeCollectionChangeNotification()
        {
            _suspendCollectionChangeNotification = false;
        }

        public void Add(IEnumerable<T> items)
        {
            if (items != null)
            {
                this.SuspendCollectionChangeNotification();

                int index = base.Count;

                try
                {
                    foreach (var i in items)
                    {
                        base.InsertItem(base.Count, i);
                    }
                }
                finally
                {
                    this.ResumeCollectionChangeNotification();

                    var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

                    this.OnCollectionChanged(arg);
                }
            }
        }
    }
}
