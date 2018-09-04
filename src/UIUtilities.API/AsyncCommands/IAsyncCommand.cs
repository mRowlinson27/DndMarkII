
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IAsyncCommand : ICommand, INotifyPropertyChanged, IDisposable
    {
        Task ExecuteAsync(object parameter);
    }
}
