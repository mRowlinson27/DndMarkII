﻿
namespace UIModel.API
{
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatModel
    {
        Task UpdateStatAsync(UiPrimaryStat primaryStat);
    }
}