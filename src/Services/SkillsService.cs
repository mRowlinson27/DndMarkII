
namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API;
    using Database.API;
    using Database.API.Dto;
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
            return await _skillsRepo.GetSkillsAsync();
        }

        public async Task AddOrUpdateSkillAsync(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}
