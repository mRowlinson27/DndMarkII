
namespace UIView.API
{
    using UIModel.API.Dto;

    public interface ISkillViewModelFactory
    {
        ISkillViewModel Create(UiSkill skill);
    }
}
