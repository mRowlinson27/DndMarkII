
namespace Database.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillsRepo
    {
        IEnumerable<Skill> GetSkills();

        void AddSkill(Skill skill);

        void AddSkills(IEnumerable<Skill> skills);

        void UpdateSkill(Skill skill);

        void UpdateSkills(IEnumerable<Skill> skills);
    }
}
