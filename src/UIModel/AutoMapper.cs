
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

        public UiSkill MapSkill(Skill svcPrimaryStat)
        {
            return new UiSkill
            {
                ArmourCheckPenalty = svcPrimaryStat.ArmourCheckPenalty,
                HasArmourCheckPenalty = svcPrimaryStat.HasArmourCheckPenalty,
                Name = svcPrimaryStat.Name,
                PrimaryStatName = IdToStringMapping[svcPrimaryStat.PrimaryStatId],
                Ranks = svcPrimaryStat.Ranks,
                Trained = svcPrimaryStat.Trained,
                UseUntrained = svcPrimaryStat.UseUntrained,
                Total = svcPrimaryStat.Total
            };
        }
    }
}
