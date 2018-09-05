
namespace UIView.ViewModel
{
    using System;
    using System.ComponentModel;
    using UIUtilities.API;

    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private readonly IUiThreadInvoker _uiThreadInvoker;

        internal ViewModelBase(IUiThreadInvoker uiThreadInvoker)
        {
            _uiThreadInvoker = uiThreadInvoker;
        }

        public bool DataAvailable
        {
            get => _dataAvailable;
            set => Set(ref _dataAvailable, value, "DataAvailable");
        }
        private bool _dataAvailable = false;

        protected bool Set<T>(ref T ipnv, T value, string ipn)
        {
            if (object.Equals(ipnv, value))
                return false;

            ipnv = value;
            OnPropertyChanged(ipn);
            return true;
        }

        protected virtual void OnPropertyChanged(string pn)
        {
            _uiThreadInvoker.Dispatch(() =>
            {
                Console.WriteLine($"DEBUG - {pn}");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pn));
            });
        }

        public virtual void Init() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract void Dispose();
    }
}
