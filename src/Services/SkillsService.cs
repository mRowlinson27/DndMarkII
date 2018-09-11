
namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using API;
    using API.Dto;
    using Database.API;
    using Utilities.API;


    public class SkillsService : ISkillsService
    {
        public event EventHandler SkillsUpdated;

        public Dictionary<Guid, Skill> CachedSvcSkills { get; set; }

        private readonly ILogger _logger;
        private readonly ISkillsRepo _skillsRepo;
        private readonly ISvcAutoMapper _svcAutoMapper;
        private readonly ISkillTotalCalculator _skillTotalCalculator;
        private readonly IPrimaryStatsService _primaryStatsService;

        public SkillsService(ILogger logger, ISkillsRepo skillsRepo, ISvcAutoMapper svcAutoMapper, ISkillTotalCalculator skillTotalCalculator, IPrimaryStatsService primaryStatsService)
        {
            _logger = logger;
            _skillsRepo = skillsRepo;
            _svcAutoMapper = svcAutoMapper;
            _skillTotalCalculator = skillTotalCalculator;
            _primaryStatsService = primaryStatsService;
            _primaryStatsService.PrimaryStatsUpdated += PrimaryStatsServiceOnPrimaryStatsUpdated;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            _logger.LogEntry();
            if (CachedSvcSkills == null)
            {
                PopulateSvcSkills();
            }

            _logger.LogExit();
            return CachedSvcSkills.Values;
        }

        public void AddSkill(Skill skill)
        {
            _logger.LogEntry();

            CachedSvcSkills.Add(skill.Id, skill);
            SkillsUpdated?.Invoke(this, EventArgs.Empty);

            _logger.LogExit();
        }

        private void PopulateSvcSkills()
        {
            var dbSkills = _skillsRepo.GetSkills();
            var svcSkills = _svcAutoMapper.MapToSvc(dbSkills);
            CachedSvcSkills = _skillTotalCalculator.AddTotals(svcSkills).ToDictionary(skill => skill.Id);
            _logger.LogEntry();
        }

        private void PrimaryStatsServiceOnPrimaryStatsUpdated(object sender, EventArgs e)
        {
            _skillTotalCalculator.AddTotals(CachedSvcSkills.Values);
            SkillsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _primaryStatsService.PrimaryStatsUpdated -= PrimaryStatsServiceOnPrimaryStatsUpdated;
        }
    }
}
