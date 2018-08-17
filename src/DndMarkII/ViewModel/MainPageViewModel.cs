
namespace NeutroniumTest.ViewModel
{
    using System;
    using Model;

    public class MainPageViewModel : ViewModelBase, IDisposable
    {
        public TitleZoneViewModel TitleZoneViewModel { get; }

        public SkillTableViewModel SkillTableViewModel { get; }

        public MainPageViewModel()
        {
            TitleZoneViewModel = new TitleZoneViewModel();
            SkillTableViewModel = new SkillTableViewModel(new SkillTableModel());
        }

        public void Dispose()
        {
            SkillTableViewModel?.Dispose();
        }
    }
}
