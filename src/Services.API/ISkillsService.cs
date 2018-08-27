﻿
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.API.Dto;

    public interface ISkillsService
    {
        event EventHandler SkillsUpdated;

        Task<IEnumerable<Skill>> GetAllSkillsAsync();

        Task AddOrUpdateSkillAsync(Skill skill);
    }
}