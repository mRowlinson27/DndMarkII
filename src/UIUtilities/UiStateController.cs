
namespace UIUtilities
{
    using System;
    using API;
    using Utilities.API;

    public class UiStateController : IUiStateController
    {
        public event EventHandler UiLockUpdated;

        public bool UiLocked { get; set; }

        private int _uiLockCount = 0;
        private readonly object _sync = new object();

        private readonly ILogger _logger;

        public UiStateController(ILogger logger)
        {
            _logger = logger;
        }

        public void IncUiLock()
        {
            lock (_sync)
            {
                _uiLockCount++;
                CheckLockStatus();
            }
        }

        public void DecUiLock()
        {
            lock (_sync)
            {
                _uiLockCount--;

                if (_uiLockCount < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                CheckLockStatus();
            }
        }

        private void CheckLockStatus()
        {
            var currentStatus = UiLocked;

            UiLocked = _uiLockCount != 0;

            if (currentStatus != UiLocked)
            {
                _logger.LogMessage($"UiLocked changed to {UiLocked}");
                UiLockUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
