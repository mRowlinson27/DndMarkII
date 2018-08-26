
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using Database.API.Dto;

    public interface ISkillsService
    {
        event EventHandler SkillsUpdated;

        IEnumerable<Skill> GetAllSkills();

        void AddOrUpdateSkill(Skill skill);
    }
}
