
namespace UIView.API
{
    using UIModel.API.Dto;

    public interface IPrimaryStatViewModelFactory
    {
        IPrimaryStatViewModel Create(UiPrimaryStat primaryStat);
    }
}
