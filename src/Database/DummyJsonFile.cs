
namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Dto;
    using Utilities.API.DAL;

    public class DummyJsonFile : IJsonFile<Model>
    {
        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

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
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Appraise",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Bluff",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Climb",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Str,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Diplomacy",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Disable Device",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Disguise",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Escape Artist",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Fly",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Handle Animal",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Heal",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Intimidate",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Linguistics",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Perception",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Ride",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Sense Motive",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Sleight of Hand",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Spellcraft",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Stealth",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Survival",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Swim",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Str,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Use Magic Device",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (arcana)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (dungeoneering)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (engineering)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (geography)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (history)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (local)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (nature)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (nobility)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (planes)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Knowledge (religion)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Name = "Profession",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0,
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
                    Id = AbilityType.Str,
                    AbilityScore = _generator.Next(4, 21),
                },
                new PrimaryStat
                {
                    Name = "Constitution",
                    Id = AbilityType.Con,
                    AbilityScore = _generator.Next(4, 21),
                },
                new PrimaryStat
                {
                    Name = "Dexterity",
                    Id = AbilityType.Dex,
                    AbilityScore = _generator.Next(4, 21),
                }
                ,new PrimaryStat
                {
                    Name = "Wisdom",
                    Id = AbilityType.Wis,
                    AbilityScore = _generator.Next(4, 21),
                },
                new PrimaryStat
                {
                    Name = "Charisma",
                    Id = AbilityType.Cha,
                    AbilityScore = _generator.Next(4, 21),
                },
                new PrimaryStat
                {
                    Name = "Intelligence",
                    Id = AbilityType.Int,
                    AbilityScore = _generator.Next(4, 21),
                }
            };
        }
    }
}
