
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;
    using Services.API.Dto;
    using Utilities.API;

    public class SkillTableModel : ISkillTableModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILogger _logger;

        private readonly ISkillsService _skillsService;

        private readonly IAutoMapper _autoMapper;

        public SkillTableModel(ILogger logger, ISkillsService skillsService, IAutoMapper autoMapper)
        {
            _logger = logger;
            _skillsService = skillsService;
            _autoMapper = autoMapper;

            _skillsService.SkillsUpdated += SkillsServiceOnSkillsUpdated;
        }

        public async Task<IEnumerable<UiSkill>> RequestSkillsAsync()
        {
            _logger.LogEntry();
            var svcSkills = await _skillsService.GetAllSkillsAsync().ConfigureAwait(false);
            var uiSkills = _autoMapper.Map(svcSkills);
            _logger.LogExit();
            return uiSkills;
        }

        public async Task AddSkillAsync()
        {
            await _skillsService.AddSkillAsync(new Skill
            {
                Id = Guid.NewGuid(),
                ArmourCheckPenalty = 0,
                HasArmourCheckPenalty = false,
                Name = "",
                PrimaryStatId = AbilityType.Str,
                Ranks = 0,
                Trained = false,
                UseUntrained = true
            });
        }

        public async Task RemoveSkillAsync(UiSkill uiSkill)
        {
            await Task.Run(() => UpdateBackEnd()).ConfigureAwait(false);
//            _skills.Remove(uiSkill);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        private void UpdateBackEnd()
        {

        }

        private void SkillsServiceOnSkillsUpdated(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
        }

        public void Dispose()
        {
            _skillsService.SkillsUpdated -= SkillsServiceOnSkillsUpdated;
        }
    }
}
