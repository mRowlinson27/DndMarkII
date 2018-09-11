
namespace UIUtilities
{
    using API;

    public interface IUiLockerContextFactory
    {
        IUiLockerContext Create(IUiStateController uiStateController);
    }
}
