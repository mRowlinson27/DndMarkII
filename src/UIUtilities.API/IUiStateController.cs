
namespace UIUtilities.API
{
    using System;

    public interface IUiStateController
    {
        event EventHandler UiLockUpdated;

        bool UiLocked { get; }

        void IncUiLock();

        void DecUiLock();

        IUiLockerContext LockedContext();
    }
}
