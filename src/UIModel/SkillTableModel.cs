
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;
    using Utilities.API;

    public class SkillTableModel : ISkillTableModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<UiSkill> _skills;

        private readonly ILogger _logger;

        private readonly ISkillsService _skillsService;

        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

        public SkillTableModel(ILogger logger, ISkillsService skillsService)
        {
            _logger = logger;
            _skillsService = skillsService;
            _skills = new List<UiSkill>
            {
                new UiSkill
                {
                    Name = "New UiSkill",
                    Modifier = "Con",
                    Total = _generator.Next(0, 20)
                }
            };
        }

        public async Task<IEnumerable<UiSkill>> RequestSkillsAsync()
        {
            _logger.LogEntry();
            await Task.Delay(_generator.Next(0, 4000)).ConfigureAwait(false);
            _logger.LogExit();
            return _skills;
        }

        public async Task AddSkillAsync()
        {
            _skills.Clear();
            _skills.Add(await Task.Run(() => GetBlankSkill()).ConfigureAwait(false));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        public async Task RemoveSkillAsync(UiSkill uiSkill)
        {
            await Task.Run(() => UpdateBackEnd()).ConfigureAwait(false);
            _skills.Remove(uiSkill);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        private UiSkill GetBlankSkill()
        {
            return new UiSkill
            {
                Name = "New UiSkill",
                Modifier = "Con",
                Total = _generator.Next(0, 20)
            };
        }

        private void UpdateBackEnd()
        {

        }

    }
}
