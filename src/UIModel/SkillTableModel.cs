
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Utilities.API;

    public class SkillTableModel : ISkillTableModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<UiSkill> _skills;

        private readonly ILogger _logger;

        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

        public SkillTableModel(ILogger logger)
        {
            _logger = logger;
            _skills = GenerateSkills();
        }

        public async Task<IEnumerable<UiSkill>> RequestSkillsAsync()
        {
            _logger.LogEntry();
            await Task.Delay(_generator.Next(0, 4000)).ConfigureAwait(true);
            _logger.LogExit();
            return _skills;
        }

        public async Task AddSkillAsync()
        {
            _skills.Clear();
            _skills.Add(await Task.Run(() => GetBlankSkill()));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        public async Task RemoveSkillAsync(UiSkill uiSkill)
        {
            await Task.Run(() => UpdateBackEnd());
            _skills.Remove(uiSkill);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        private UiSkill GetBlankSkill()
        {
            return new UiSkill
            {
                Name = "New UiSkill",
                Modifier = AbilityModifier.Con,
                Total = _generator.Next(0, 20)
            };
        }

        private void UpdateBackEnd()
        {

        }

        private List<UiSkill> GenerateSkills()
        {
            var result = new List<UiSkill>
            {
                new UiSkill()
                {
                    Name = "Acrobatics",
                    Ranks = 0,
                    HasArmourCheckPenalty = true,
                    ArmourCheckPenalty = 0,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = true
                },
                new UiSkill()
                {
                    Name = "Appraise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Bluff",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Climb",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Diplomacy",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Disable Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Disguise",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Escape Artist",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Fly",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Handle Animal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Heal",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Intimidate",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Linguistics",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Perception",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Ride",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Sense Motive",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Sleight of Hand",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Spellcraft",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Stealth",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Dex,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Survival",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Swim",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Str,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Use Magic Device",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Cha,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (arcana)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (dungeoneering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (engineering)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (geography)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (history)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (local)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (nature)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (nobility)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (planes)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Knowledge (religion)",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Int,
                    UseUntrained = true,
                    Trained = false
                },
                new UiSkill()
                {
                    Name = "Profession",
                    Ranks = 0,
                    HasArmourCheckPenalty = false,
                    Modifier = AbilityModifier.Wis,
                    UseUntrained = true,
                    Trained = false,
                }
            };

            foreach (var skill in result)
            {
                skill.Total = _generator.Next(0, 20);
            }

            return result;
        }
    }
}
