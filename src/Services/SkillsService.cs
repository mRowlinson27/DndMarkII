
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

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            _logger.LogEntry();

            var dbSkills = await _skillsRepo.GetSkillsAsync().ConfigureAwait(false);
            var svcSkills = _svcAutoMapper.MapToSvc(dbSkills);
            var svcSkillsWithTotal = await _skillTotalCalculator.AddTotalsAsync(svcSkills);

            _logger.LogExit();
            return svcSkillsWithTotal;
        }

        public async Task AddSkillAsync(Skill skill)
        {
            _logger.LogEntry();

            await _skillsRepo.AddSkillAsync(_svcAutoMapper.MapToDb(skill));
            SkillsUpdated?.Invoke(this, EventArgs.Empty);

            _logger.LogExit();
        }
    }
}
