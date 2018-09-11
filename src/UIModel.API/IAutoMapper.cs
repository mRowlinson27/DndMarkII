
namespace UIModel.API
{
    using System.Collections.Generic;
    using Dto;
    using Services.API.Dto;

    public interface IAutoMapper
    {
        UiPrimaryStat MapToUi(PrimaryStat svcPrimaryStat);

        UiSkill MapToUi(Skill svcSkill);

        IEnumerable<UiPrimaryStat> MapToUi(IEnumerable<PrimaryStat> svcPrimaryStats);

        IEnumerable<UiSkill> MapToUi(IEnumerable<Skill> svcSkills);

        PrimaryStatUpdateRequest MapToSvcRequest(UiPrimaryStat uiPrimaryStat);

        Skill MapToSvcRequest(UiSkill uiSkill);

        IEnumerable<PrimaryStatUpdateRequest> MapToSvcRequest(IEnumerable<UiPrimaryStat> uiPrimaryStats);

        IEnumerable<Skill> MapToSvcRequest(IEnumerable<UiSkill> uiSkills);
    }
}
