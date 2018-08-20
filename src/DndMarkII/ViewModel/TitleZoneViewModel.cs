
namespace DndMarkII.ViewModel
{
    using UIModel.API;

    public class TitleZoneViewModel : ViewModelBase
    {
        public string Title => "Title";

        private readonly ITitleZoneModel _model;

        public TitleZoneViewModel(ITitleZoneModel model)
        {
            _model = model;
        }
    }
}
