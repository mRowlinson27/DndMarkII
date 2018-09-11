
namespace UIView.ViewModel
{
    using UIModel.API;
    using UIUtilities.API;

    public class TitleZoneViewModel : ViewModelBase
    {
        public string Title => "Title";

        private readonly ITitleZoneModel _model;

        public TitleZoneViewModel(ITitleZoneModel model, IUiThreadInvoker uiThreadInvoker) : base(uiThreadInvoker)
        {
            _model = model;
        }

        public override void Dispose()
        {
            
        }
    }
}
