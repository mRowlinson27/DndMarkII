
namespace UIUtilities.API.AsyncCommands
{
    using System.Windows.Input;

    public interface IAsyncCommandAdaptor : ICommand
    {
        bool ShouldExecute { get; set; }
    }
}
