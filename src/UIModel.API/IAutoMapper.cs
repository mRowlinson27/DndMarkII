
namespace UIModel.API
{
    using System.Collections.Generic;
    using Dto;
    using Services.API.Dto;

    public interface IAutoMapper
    {
        IEnumerable<UiPrimaryStat> Map(IEnumerable<PrimaryStat> svcPrimaryStat);

        IEnumerable<UiSkill> Map(IEnumerable<Skill> svcSkill);
    }
}
