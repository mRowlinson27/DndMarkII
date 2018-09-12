
namespace UIView.API
{
    using UIModel.API.Dto;
    using UIUtilities.API;

    public interface ISkillViewModelFactory
    {
        ISkillViewModel Create(UiSkill uiSkill);
    }
}
