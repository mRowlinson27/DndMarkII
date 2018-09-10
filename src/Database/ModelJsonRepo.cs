
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

        public IEnumerable<PrimaryStat> GetPrimaryStats()
        {
            var model = GetModel();
            return model.PrimaryStats;
        }

        public void UpdatePrimaryStats(IEnumerable<PrimaryStat> stats)
        {
            var statsList = stats.ToList();

            var model = GetModel();
            model.PrimaryStats = statsList;
        }

        public IEnumerable<Skill> GetSkills()
        {
            var model = GetModel();
            return model.Skills.Values;
        }

        public void AddSkill(Skill skill)
        {
            var model = GetModel();
            model.Skills.Add(skill.Id, skill);
        }

        public void AddSkills(IEnumerable<Skill> skills)
        {
            foreach (var skill in skills)
            {
                AddSkill(skill);
            }
        }

        public void UpdateSkill(Skill skill)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSkills(IEnumerable<Skill> skills)
        {
            var model = GetModel();

            var skillDict = skills.ToDictionary(skill => skill.Id);

            model.Skills = skillDict;
        }

        private Model GetModel()
        {
            return _model ?? (_model = _databaseFile.Read());
        }

        private async Task SaveModelAsync()
        {
            await _databaseFile.WriteAsync(_model);
        }
    }
}
