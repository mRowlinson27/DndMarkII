
namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var skills = GenerateSkills();
            var skillDict = skills.ToDictionary(skill => skill.Id);

            var model = new Model
            {
                Skills = skillDict,
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
                    Id = Guid.NewGuid(),
                    Name = "Acrobatics",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Appraise",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Bluff",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Climb",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Str,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Diplomacy",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Disable Device",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Disguise",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Escape Artist",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fly",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Handle Animal",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Heal",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Intimidate",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Linguistics",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Perception",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ride",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sense Motive",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Sleight of Hand",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Spellcraft",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Stealth",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Dex,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Survival",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Wis,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Swim",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Str,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Use Magic Device",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Cha,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (arcana)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (dungeoneering)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (engineering)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (geography)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (history)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (local)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (nature)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (nobility)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (planes)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
                    Name = "Knowledge (religion)",
                    Ranks = _generator.Next(0, 11),
                    HasArmourCheckPenalty = _generator.Next(0, 2) > 0,
                    PrimaryStatId = AbilityType.Int,
                    UseUntrained = _generator.Next(0, 2) > 0,
                    Trained = _generator.Next(0, 2) > 0
                },
                new Skill()
                {
                    Id = Guid.NewGuid(),
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
