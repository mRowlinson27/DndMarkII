
namespace UIView.API
{
    using System;

    public interface ISkillViewModel
    {
        Guid Id { get; }

        int Total { get; }

        string Name { get; }

        int Ranks { get; }

        string PrimaryStatName { get; }

        string PrimaryStatModifier { get; }

        bool HasArmourCheckPenalty { get; }

        int ArmourCheckPenalty { get; }

        bool UseUntrained { get; }

        bool Class { get; }

        bool ShowingDetails { get; }

        string BackGroundColour { get; set; }
    }
}
