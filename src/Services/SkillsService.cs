
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

        public SkillsService(ILogger logger, ISkillsRepo skillsRepo, ISvcAutoMapper svcAutoMapper)
        {
            _logger = logger;
            _skillsRepo = skillsRepo;
            _svcAutoMapper = svcAutoMapper;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            _logger.LogEntry();

            var dbSkills = await _skillsRepo.GetSkillsAsync().ConfigureAwait(false);
            var svcSkills = _svcAutoMapper.Map(dbSkills);
            var svcSkillsWithTotal = svcSkills.Select(AddTotalToSkill);

            _logger.LogExit();
            return svcSkillsWithTotal;
        }

        public async Task AddOrUpdateSkillAsync(Skill skill)
        {
            throw new NotImplementedException();
        }

        private Skill AddTotalToSkill(Skill skill)
        {
            skill.Total = skill.Ranks;
            if (skill.Trained && skill.Ranks > 0)
            {
                skill.Total += 3;
            }

            return skill;
        }
    }
}
