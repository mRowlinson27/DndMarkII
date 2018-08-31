
namespace UIModel.API.Dto
{
    using System;

    public class UiSkill
    {
        public Guid Id { get; set; }

        public int Total { get; set; }

        public string Name { get; set; }

        public int Ranks { get; set; }

        public string PrimaryStatName { get; set; }

        public bool HasArmourCheckPenalty { get; set; }

        public int ArmourCheckPenalty { get; set; }

        public bool UseUntrained { get; set; }

        public bool Trained { get; set; }
    }
}
