
namespace UIView.API
{
    using UIModel.API.Dto;

    public interface IPrimaryStatViewModel
    {
        string ShortName { get; }

        string Name { get; }

        string AbilityScore { get; set; }

        string AbilityModifier { get; }

        bool InEdit { get; set; }

        UiPrimaryStat PrimaryStat { get; set; }
    }
}
