
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

        private static readonly Dictionary<string, AbilityType> StringToIdMapping = new Dictionary<string, AbilityType>
        {
            {"CHA", AbilityType.Cha},
            {"CON", AbilityType.Con},
            {"DEX", AbilityType.Dex},
            {"INT", AbilityType.Int},
            {"WIS", AbilityType.Wis},
            {"STR", AbilityType.Str},
        };

        public UiPrimaryStat MapToUi(PrimaryStat svcPrimaryStat)
        {
            var result = new UiPrimaryStat
            {
                Name = svcPrimaryStat.Name,
                ShortName = IdToStringMapping[svcPrimaryStat.Id],
                AbilityScore = svcPrimaryStat.AbilityScore.ToString(),
                AbilityModifier = CreateUiAbilityModifier(svcPrimaryStat.AbilityModifier),
            };

            return result;
        }

        public UiSkill MapToUi(Skill svcSkill)
        {
            return new UiSkill
            {
                ArmourCheckPenalty = svcSkill.ArmourCheckPenalty,
                HasArmourCheckPenalty = svcSkill.HasArmourCheckPenalty,
                Name = svcSkill.Name,
                PrimaryStatName = IdToStringMapping[svcSkill.PrimaryStatId],
                Ranks = svcSkill.Ranks.ToString(),
                Class = svcSkill.Class,
                UseUntrained = svcSkill.UseUntrained,
                Total = svcSkill.Total,
                Id = svcSkill.Id,
                PrimaryStatModifier = CreateUiAbilityModifier(svcSkill.PrimaryStatModifier)
            };
        }

        public IEnumerable<UiPrimaryStat> MapToUi(IEnumerable<PrimaryStat> svcPrimaryStats)
        {
            return svcPrimaryStats.Select(MapToUi);
        }

        public IEnumerable<UiSkill> MapToUi(IEnumerable<Skill> svcSkill)
        {
            return svcSkill.Select(MapToUi);
        }

        public PrimaryStatUpdateRequest MapToSvcRequest(UiPrimaryStat uiPrimaryStat)
        {
            return new PrimaryStatUpdateRequest
            {
                AbilityScore = int.Parse(uiPrimaryStat.AbilityScore),
                Id = StringToIdMapping[uiPrimaryStat.ShortName]
            };
        }

        public SkillUpdateRequest MapToSvcRequest(UiSkill uiSkill)
        {
            return new SkillUpdateRequest
            {
                Id = uiSkill.Id,
                Ranks = int.Parse(uiSkill.Ranks),
                Class = uiSkill.Class
            };
        }

        public IEnumerable<PrimaryStatUpdateRequest> MapToSvcRequest(IEnumerable<UiPrimaryStat> uiPrimaryStat)
        {
            return uiPrimaryStat.Select(MapToSvcRequest);
        }

        public IEnumerable<SkillUpdateRequest> MapToSvcRequest(IEnumerable<UiSkill> uiSkills)
        {
            return uiSkills.Select(MapToSvcRequest);
        }

        private static string CreateUiAbilityModifier(int abilityModifier)
        {
            if (abilityModifier > 0)
            {
                return "+" + abilityModifier;
            }

            return abilityModifier.ToString();
        }
    }
}
