
namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Utilities.API.DAL;

    public class ModelJsonRepo : IMetadataRepo, IPrimaryStatsRepo, ISkillsRepo
    {
        private readonly IJsonFile<Model> _databaseFile;

        private Model _model;

        public ModelJsonRepo(IJsonFile<Model> databaseFile)
        {
            _databaseFile = databaseFile;
        }

        public async Task<IEnumerable<PrimaryStat>> GetPrimaryStatsAsync()
        {
            var model = await GetModel().ConfigureAwait(false);
            return model.PrimaryStats;
        }

        public async Task UpdatePrimaryStatsAsync(IEnumerable<PrimaryStat> stats)
        {
            var statsList = stats.ToList();

            var model = await GetModel().ConfigureAwait(false);
            model.PrimaryStats = statsList;
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            var model = await GetModel().ConfigureAwait(false);
            return model.Skills.Values;
        }

        public async Task AddSkillAsync(Skill skill)
        {
            var model = await GetModel().ConfigureAwait(false);
            model.Skills.Add(skill.Id, skill);
        }

        public async Task AddSkillsAsync(IEnumerable<Skill> skills)
        {
            foreach (var skill in skills)
            {
                await AddSkillAsync(skill);
            }
        }

        public Task UpdateSkillAsync(Skill skill)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateSkillsAsync(IEnumerable<Skill> skills)
        {
            var model = await GetModel().ConfigureAwait(false);

            var skillDict = skills.ToDictionary(skill => skill.Id);

            model.Skills = skillDict;
        }

        private async Task<Model> GetModel()
        {
            return _model ?? (_model = await _databaseFile.ReadAsync());
        }

        private async Task SaveModelAsync()
        {
            await _databaseFile.WriteAsync(_model);
        }
    }
}
