
namespace UIModel.API.Dto
{
    public class Skill
    {
        public int Total { get; set; }

        public string Name { get; set; }

        public int Ranks { get; set; }

        public bool HasArmourCheckPenalty { get; set; }

        public int ArmourCheckPenalty { get; set; }

        public bool UseUntrained { get; set; }

        public bool Trained { get; set; }

        public AbilityModifier Modifier { get; set; }
    }

    public enum AbilityModifier { Str, Dex, Con, Int, Wis, Cha };
}
