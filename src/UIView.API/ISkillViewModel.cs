
namespace UIView.API
{
    public interface ISkillViewModel
    {
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
