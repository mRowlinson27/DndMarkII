
namespace UIModel
{
    using System.Collections.Generic;
    using System.Linq;
    using API;
    using API.Dto;
    using Services.API.Dto;

    public class AutoMapper : IAutoMapper
    {
        private static readonly Dictionary<AbilityType, string> IdToStringMapping = new Dictionary<AbilityType, string>
        {
            {AbilityType.Cha, "CHA"},
            {AbilityType.Con, "CON"},
            {AbilityType.Dex, "DEX"},
            {AbilityType.Int, "INT"},
            {AbilityType.Wis, "WIS"},
            {AbilityType.Str, "STR"},
        };

        public IEnumerable<UiPrimaryStat> Map(IEnumerable<PrimaryStat> svcPrimaryStats)
        {
            return svcPrimaryStats.Select(MapPrimaryStat);
        }

        public IEnumerable<UiSkill> Map(IEnumerable<Skill> svcSkill)
        {
            return svcSkill.Select(MapSkill);
        }

        private UiPrimaryStat MapPrimaryStat(PrimaryStat svcPrimaryStat)
        {
            var result = new UiPrimaryStat()
            {
                Name = svcPrimaryStat.Name,
                ShortName = IdToStringMapping[svcPrimaryStat.Id],
                AbilityScore = svcPrimaryStat.AbilityScore.ToString(),
            };

            if (svcPrimaryStat.AbilityModifier > 0)
            {
                result.AbilityModifier = "+" + svcPrimaryStat.AbilityModifier;
            }
            else
            {
                result.AbilityModifier = svcPrimaryStat.AbilityModifier.ToString();
            }

            return result;
        }

        public UiSkill MapSkill(Skill svcSkill)
        {
            return new UiSkill
            {
                ArmourCheckPenalty = svcSkill.ArmourCheckPenalty,
                HasArmourCheckPenalty = svcSkill.HasArmourCheckPenalty,
                Name = svcSkill.Name,
                PrimaryStatName = IdToStringMapping[svcSkill.PrimaryStatId],
                Ranks = svcSkill.Ranks,
                Trained = svcSkill.Trained,
                UseUntrained = svcSkill.UseUntrained,
                Total = svcSkill.Total,
                Id = svcSkill.Id
            };
        }
    }
}
