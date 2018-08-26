
namespace Database
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API;
    using API.Dto;

    public class ModelJsonRepo : IMetadataRepo, IPrimaryStatsRepo, ISkillsRepo
    {
        private Model _model;

        public ModelJsonRepo()
        {

        }

        public Task<IEnumerable<PrimaryStat>> GetPrimaryStatsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSkillsAsync(IEnumerable<Skill> skills)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePrimaryStatsAsync(IEnumerable<PrimaryStat> stats)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            throw new System.NotImplementedException();
        }

    }
}
