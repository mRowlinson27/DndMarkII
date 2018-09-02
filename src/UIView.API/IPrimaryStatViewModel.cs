
namespace UIView.API
{
    public interface IPrimaryStatViewModel
    {
        string ShortName { get; }

        string Name { get; }

        string AbilityScore { get; set; }

        string AbilityModifier { get; }

        bool InEdit { get; set; }
    }
}
