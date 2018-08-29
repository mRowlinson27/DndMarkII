
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

        bool HasArmourCheckPenalty { get; }

        int ArmourCheckPenalty { get; }

        bool UseUntrained { get; }

        bool Trained { get; }

        bool ShowDetails { get; }
    }
}
