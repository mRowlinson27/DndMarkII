
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        public IEnumerable<UiSkill> RequestSkills()
        {
            _logger.LogEntry();
            var svcSkills = _skillsService.GetAllSkills();
            var uiSkills = _autoMapper.MapToUi(svcSkills);
            _logger.LogExit();
            return uiSkills;
        }

        public void AddSkill()
        {
            _skillsService.AddSkill(new Skill
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

        public void RemoveSkill(UiSkill uiSkill)
        {
            UpdateBackEnd();
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
