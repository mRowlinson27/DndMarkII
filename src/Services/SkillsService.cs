
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

        public SkillsService(ILogger logger, ISkillsRepo skillsRepo)
        {
            _logger = logger;
            _skillsRepo = skillsRepo;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            var dbSkills = await _skillsRepo.GetSkillsAsync();
            return CalculateSkills(dbSkills);
        }

        public async Task AddOrUpdateSkillAsync(Skill skill)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Skill> CalculateSkills(IEnumerable<Database.API.Dto.Skill> dbSkills)
        {
            var svcSkills = dbSkills.Select(dbSkill => new Skill(dbSkill));
            return svcSkills;
        }
    }
}
