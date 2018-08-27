
namespace Database
{
    using System.Collections.Generic;
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
            var model = await GetModel();
            return model.PrimaryStats;
        }

        public async Task UpdatePrimaryStatsAsync(IEnumerable<PrimaryStat> stats)
        {
            var model = await GetModel();
            model.PrimaryStats = stats;
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            var model = await GetModel();
            return model.Skills;
        }

        public async Task UpdateSkillsAsync(IEnumerable<Skill> skills)
        {
            var model = await GetModel();
            model.Skills = skills;
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
