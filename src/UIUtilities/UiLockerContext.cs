
namespace UIUtilities
{
    using API;

    public class UiLockerContext : IUiLockerContext
    {
        private readonly IUiStateController _uiStateController;

        public UiLockerContext(IUiStateController uiStateController)
        {
            _uiStateController = uiStateController;
            _uiStateController.IncUiLock();
        }

        public void Dispose()
        {
            _uiStateController.DecUiLock();
        }
    }
}
