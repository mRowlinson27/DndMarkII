
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API.AsyncCommands;
    using Neutronium.MVVMComponents;

    public class AsyncSimpleCommandAdaptor : IAsyncCommandAdaptor, ICommandWithoutParameter
    {
        public event EventHandler CanExecuteChanged;

        public bool ShouldExecute
        {
            get => _shouldExecute;
            set
            {
                if (_shouldExecute == value)
                    return;

                _shouldExecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private readonly IAsyncCommand _asyncCommand;
        private bool _shouldExecute;

        public AsyncSimpleCommandAdaptor( IAsyncCommand asyncCommand)
        {
            _asyncCommand = asyncCommand;
            _shouldExecute = _asyncCommand.CanExecute(null);
            _asyncCommand.CanExecuteChanged += AsyncCommandOnCanExecuteChanged;
        }

        bool ICommandWithoutParameter.CanExecute => CanExecute(null);
        public bool CanExecute(object parameter) => _shouldExecute;

        public void Execute() => Execute(null);
        public void Execute(object parameter) => ExecuteAsync();

        public async Task ExecuteAsync()
        {
            if (!_shouldExecute)
                return;

            await _asyncCommand.ExecuteAsync(null);
        }

        private void AsyncCommandOnCanExecuteChanged(object sender, EventArgs e)
        {
            ShouldExecute = _asyncCommand.CanExecute(null);
        }

        public void Dispose()
        {
            _asyncCommand.CanExecuteChanged -= AsyncCommandOnCanExecuteChanged;
            _asyncCommand.Dispose();
        }
    }
}
