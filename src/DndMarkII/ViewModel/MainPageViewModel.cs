
namespace DndMarkII.ViewModel
{
    using System;

    public class MainPageViewModel : ViewModelBase, IDisposable
    {
        public TitleZoneViewModel TitleZoneViewModel { get; set; }

        public SkillTableViewModel SkillTableViewModel { get; set; }

        public void Dispose()
        {
            SkillTableViewModel?.Dispose();
        }
    }
}
