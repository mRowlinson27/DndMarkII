
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;

    public class SkillTableModel : ISkillTableModel
    {
        public IList<Skill> Skills => _skills;

        private readonly ObservableCollection<Skill> _skills;

        public SkillTableModel()
        {
            _skills = new ObservableCollection<Skill>
            {
                new Skill()
                {
                    Name = "Acrobatics",
                    Ranks = 0,
                    HasArmourCheckPenalty = true,
                    ArmourCheckPenalty = 0,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = true
                },
                new Skill()
                {
                    Name = "Appraise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Bluff",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Climb",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Diplomacy",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Disable Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Disguise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Escape Artist",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Fly",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Handle Animal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Heal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Intimidate",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Linguistics",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Perception",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Ride",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Sense Motive",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Sleight of Hand",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Spellcraft",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Stealth",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Survival",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Swim",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Use Magic Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (arcana)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (dungeoneering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (engineering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (geography)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (history)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (local)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (nature)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (nobility)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (planes)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Knowledge (religion)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new Skill()
                {
                    Name = "Profession",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false,
                }
            };

            foreach (var skill in _skills)
            {
                skill.Total = Generator.Next(0, 20);
            }
        }

        public async Task AddSkillAsync()
        {
            _skills.Add(await Task.Run(() => GetBlankSkill()));
        }

        public async Task RemoveSkillAsync(Skill skill)
        {
            await Task.Run(() => UpdateBackEnd());
            _skills.Remove(skill);
        }

        static readonly Random Generator = new Random(DateTime.Now.Millisecond);

        private Skill GetBlankSkill()
        {
            return new Skill
            {
                Name = "New Skill",
                Modifier = AbilityModifier.Con,
                Total = Generator.Next(0, 20)
            };
        }

        private void UpdateBackEnd()
        {

        }
    }
}
