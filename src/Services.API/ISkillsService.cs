
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillsService
    {
        event EventHandler SkillsUpdated;

        IEnumerable<Skill> GetAllSkills();

        void AddSkill(Skill skill);
    }
}
