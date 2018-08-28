
namespace Services.API.Dto
{
    using System;

    public class Skill
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Ranks { get; set; }

        public bool HasArmourCheckPenalty { get; set; }

        public int ArmourCheckPenalty { get; set; }

        public bool UseUntrained { get; set; }

        public bool Trained { get; set; }

        public Services.API.Dto.AbilityType PrimaryStatId { get; set; }

        public int Total { get; set; }
    }
}
