
namespace DndMarkII.ViewModel
{
    using System;
    using UIModel.API;

    public class MainPageViewModel : ViewModelBase, IDisposable
    {
        public TitleZoneViewModel TitleZoneViewModel { get; set; }

        public SkillTableViewModel SkillTableViewModel { get; set; }

        private readonly IMainPageModel _model;

        public MainPageViewModel(IMainPageModel model)
        {
            _model = model;
        }

        public void Dispose()
        {
            SkillTableViewModel?.Dispose();
        }
    }
}
