using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntelMAS.UI.Helpers
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class NotifyBaseCollection<T> : ObservableCollection<T>
    {
        protected virtual bool SetProperty<M>(ref M storage, M value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<M>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
}

