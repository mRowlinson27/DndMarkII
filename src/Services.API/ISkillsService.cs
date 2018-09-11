
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using Dto;

    public interface ISkillsService : IDisposable
    {
        event EventHandler SkillsUpdated;

        IEnumerable<Skill> GetAllSkills();

        void AddSkill(Skill skill);

        void UpdateSkill(SkillUpdateRequest skillUpdateRequest);
    }
}
