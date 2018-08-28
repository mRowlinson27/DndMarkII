
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

        public Skill()
        {

        }

        public Skill(Database.API.Dto.Skill dbSkill)
        {
            Id = dbSkill.Id;
            Name = dbSkill.Name;
            Ranks = dbSkill.Ranks;
            HasArmourCheckPenalty = dbSkill.HasArmourCheckPenalty;
            UseUntrained = dbSkill.UseUntrained;
            Trained = dbSkill.Trained;
            PrimaryStatId = (AbilityType) dbSkill.PrimaryStatId;
        }
    }
}
