
namespace Database.API
{
    using System.Collections.Generic;
    using Dto;

    public interface ISkillsRepo
    {
        IEnumerable<Skill> Get();

        void Update(IEnumerable<Skill> skills);
    }
}
