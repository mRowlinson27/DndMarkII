
namespace UIModel
{
    using API;
    using API.Dto;
    using Services.API;

    public class SkillModel : ISkillModel
    {
        private readonly ISkillsService _skillsService;
        private readonly IAutoMapper _autoMapper;

        public SkillModel(ISkillsService skillsService, IAutoMapper autoMapper)
        {
            _skillsService = skillsService;
            _autoMapper = autoMapper;
        }

        public void Update(UiSkill uiSkill)
        {
            _skillsService.UpdateSkill(_autoMapper.MapToSvcRequest(uiSkill));
        }
    }
}
