
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;

    public class SkillTableModel : ISkillTableModel
    {
        public IList<Skill> Skills => _skills;

        private readonly ObservableCollection<Skill> _skills;

        public SkillTableModel()
        {
            _skills = new ObservableCollection<Skill>
            {
                new Skill
                {
                    Name = "Acro",
                    Type = "Dex",
                    Total = Generator.Next(0, 20)
                },
                new Skill
                {
                    Name = "Jump",
                    Type = "Str",
                    Total = Generator.Next(0, 20)
                }
            };
        }

        public async Task AddSkillAsync()
        {
            _skills.Add(await Task.Run(() => GetBlankSkill()));
        }

        public async Task RemoveSkillAsync(Skill skill)
        {
            await Task.Run(() => UpdateBackEnd());
            _skills.Remove(skill);
        }

        static readonly Random Generator = new Random(DateTime.Now.Millisecond);

        private Skill GetBlankSkill()
        {
            return new Skill
            {
                Name = "New Skill",
                Type = "Str",
                Total = Generator.Next(0, 20)
            };
        }

        private void UpdateBackEnd()
        {

        }
    }
}
