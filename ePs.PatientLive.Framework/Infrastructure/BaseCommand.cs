using System;
using System.Windows.Input;
using Windows.UI.Xaml.Input;

namespace ePs.PatientLive.Framework.Infrastructure
{
    public class BaseCommand<T> : ICommand
    {
        readonly Action<T> callback;
        public event EventHandler CanExecuteChanged;

        public BaseCommand(Action<T> handler)
        {
            callback = handler;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (callback != null) callback((T)parameter);
        }
    }
}