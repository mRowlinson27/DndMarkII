
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.ComponentModel;

    public interface IWatchableCommandProperties<TResult> : INotifyPropertyChanged, IDisposable
    {
        event EventHandler CanExecuteChanged;

        INotifyTaskCompletion<TResult> Execution { get; }

        bool CanExecute(object parameter);
    }
}
