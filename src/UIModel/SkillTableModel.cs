
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;
    using Utilities.API;

    public class SkillTableModel : ISkillTableModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILogger _logger;

        private readonly ISkillsService _skillsService;

        private readonly IAutoMapper _autoMapper;

        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

        public SkillTableModel(ILogger logger, ISkillsService skillsService, IAutoMapper autoMapper)
        {
            _logger = logger;
            _skillsService = skillsService;
            _autoMapper = autoMapper;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills"));
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

    }
}
