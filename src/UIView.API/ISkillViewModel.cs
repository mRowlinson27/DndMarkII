
namespace UIView.API
{
    using System;

    public interface ISkillViewModel
    {
        Guid Id { get; }

        int Total { get; }

        string Name { get; }

        string Ranks { get; }

        string PrimaryStatName { get; }

        string PrimaryStatModifier { get; }

        bool HasArmourCheckPenalty { get; }

        int ArmourCheckPenalty { get; }

        bool UseUntrained { get; }

        bool Class { get; set; }

        bool ShowingDetails { get; }

        string BackGroundColour { get; set; }

        bool InEdit { get; }
    }
}
