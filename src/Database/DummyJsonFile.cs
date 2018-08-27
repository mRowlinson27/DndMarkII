
namespace Database
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Dto;
    using Utilities.API.DAL;

    public class DummyJsonFile : IJsonFile<Model>
    {
        public async Task<Model> ReadAsync()
        {
            return await Task.FromResult(GenerateDummyModel());
        }

        public async Task WriteAsync(Model data)
        {
            await Task.Delay(1);
        }

        private Model GenerateDummyModel()
        {
            var model = new Model
            {
                Skills = GenerateSkills(),
                PrimaryStats = GeneratePrimaryStats()
            };

            return model;
        }

        private List<Skill> GenerateSkills()
        {
            var result = new List<Skill>
            {
                new Skill()
                {
                    Name = "Acrobatics",
                    Ranks = 0,
                    HasArmourCheckPenalty = true,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = true
                },
                new Skill()
                {
                    Name = "Appraise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Bluff",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Climb",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Diplomacy",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Disable Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Disguise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Escape Artist",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Fly",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Handle Animal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Heal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Intimidate",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Linguistics",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Perception",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Ride",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Sense Motive",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Sleight of Hand",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Spellcraft",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Stealth",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Survival",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Swim",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Use Magic Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (arcana)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (dungeoneering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (engineering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (geography)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (history)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (local)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (nature)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (nobility)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (planes)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (religion)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Profession",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    PrimaryStatId = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false,
                }
            };

            return result;
        }

        private List<PrimaryStat> GeneratePrimaryStats()
        {
            return new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Name = "Strength",
                    Id = AbilityModifier.Str,
                    AbilityScore = 16,
                },
                new PrimaryStat
                {
                    Name = "Constitution",
                    Id = AbilityModifier.Con,
                    AbilityScore = 10,
                },
                new PrimaryStat
                {
                    Name = "Dexterity",
                    Id = AbilityModifier.Dex,
                    AbilityScore = 18,
                }
                ,new PrimaryStat
                {
                    Name = "Wisdom",
                    Id = AbilityModifier.Wis,
                    AbilityScore = 8,
                },
                new PrimaryStat
                {
                    Name = "Charisma",
                    Id = AbilityModifier.Cha,
                    AbilityScore = 16,
                },
                new PrimaryStat
                {
                    Name = "Intelligence",
                    Id = AbilityModifier.Int,
                    AbilityScore = 14,
                }
            };
        }
    }
}
