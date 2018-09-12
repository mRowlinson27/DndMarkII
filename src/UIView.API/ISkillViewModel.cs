
namespace UIView.API
{
    using UIModel.API.Dto;

    public interface ISkillViewModel
    {
        int Total { get; }

        string Name { get; }

        string Ranks { get; }

        string PrimaryStatName { get; }

        string PrimaryStatModifier { get; }

        bool HasArmourCheckPenalty { get; }

        int ArmourCheckPenalty { get; }

        bool UseUntrained { get; }

        bool Class { get; set; }

        string BackGroundColour { get; set; }

        bool InEdit { get; }

        UiSkill Skill { get; set; }
    }
}
