
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
        private static readonly object Sync = new object();

        private readonly ILogger _logger;
        private readonly IUiLockerContextFactory _uiLockerContextFactory;

        public UiStateController(ILogger logger, IUiLockerContextFactory uiLockerContextFactory)
        {
            _logger = logger;
            _uiLockerContextFactory = uiLockerContextFactory;
        }

        public void IncUiLock()
        {
            lock (Sync)
            {
                _uiLockCount++;
                CheckLockStatus();
            }
        }

        public void DecUiLock()
        {
            lock (Sync)
            {
                _uiLockCount--;

                if (_uiLockCount < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                CheckLockStatus();
            }
        }

        public IUiLockerContext LockedContext()
        {
            return _uiLockerContextFactory.Create(this);
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
