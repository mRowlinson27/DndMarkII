
namespace Database.API.Dto
{
    using System;

    public class Skill
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Ranks { get; set; }

        public bool HasArmourCheckPenalty { get; set; }

        public bool UseUntrained { get; set; }

        public bool Trained { get; set; }

        public AbilityType PrimaryStatId { get; set; }
    }
}
