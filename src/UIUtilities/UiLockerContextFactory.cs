
namespace UIUtilities
{
    using API;

    public class UiLockerContextFactory : IUiLockerContextFactory
    {
        public IUiLockerContext Create(IUiStateController uiStateController)
        {
            return new UiLockerContext(uiStateController);
        }
    }
}
