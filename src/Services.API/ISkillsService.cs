﻿
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillsService
    {
        event EventHandler SkillsUpdated;

        Task<IEnumerable<Skill>> GetAllSkillsAsync();

        Task AddSkillAsync(Skill skill);
    }
}
