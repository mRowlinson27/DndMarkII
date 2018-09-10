
namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Database.API;
    using Utilities.API;


    public class SkillsService : ISkillsService
    {
        public event EventHandler SkillsUpdated;

        private readonly ILogger _logger;

        private readonly ISkillsRepo _skillsRepo;

        private readonly ISvcAutoMapper _svcAutoMapper;

        private readonly ISkillTotalCalculator _skillTotalCalculator;

        public SkillsService(ILogger logger, ISkillsRepo skillsRepo, ISvcAutoMapper svcAutoMapper, ISkillTotalCalculator skillTotalCalculator)
        {
            _logger = logger;
            _skillsRepo = skillsRepo;
            _svcAutoMapper = svcAutoMapper;
            _skillTotalCalculator = skillTotalCalculator;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            _logger.LogEntry();

            var dbSkills = _skillsRepo.GetSkills();
            var svcSkills = _svcAutoMapper.MapToSvc(dbSkills);
            var svcSkillsWithTotal = _skillTotalCalculator.AddTotals(svcSkills);

            _logger.LogExit();
            return svcSkillsWithTotal;
        }

        public void AddSkill(Skill skill)
        {
            _logger.LogEntry();

            _skillsRepo.AddSkill(_svcAutoMapper.MapToDb(skill));
            SkillsUpdated?.Invoke(this, EventArgs.Empty);

            _logger.LogExit();
        }
    }
}
