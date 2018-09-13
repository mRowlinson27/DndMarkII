
namespace UIModel
{
    using API;
    using Services.API;

    public class SkillModelFactoryFactory : ISkillModelFactory
    {
        private readonly ISkillsService _skillsService;
        private readonly IAutoMapper _autoMapper;

        public SkillModelFactoryFactory(ISkillsService skillsService, IAutoMapper autoMapper)
        {
            _skillsService = skillsService;
            _autoMapper = autoMapper;
        }

        public ISkillModel Create()
        {
            return new SkillModel(_skillsService, _autoMapper);
        }
    }
}
