
namespace Services.API.Dto
{
    public class PrimaryStat
    {
        public AbilityModifier Id { get; set; }

        public string Name { get; set; }

        public int AbilityScore { get; set; }

        public int AbilityModifier { get; set; }

        public PrimaryStat()
        {

        }

        public PrimaryStat(Database.API.Dto.PrimaryStat primaryStat)
        {
            Id = (AbilityModifier) primaryStat.Id;
            Name = primaryStat.Name;
            AbilityScore = primaryStat.AbilityScore;
        }
    }
}
