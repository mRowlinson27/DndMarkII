
namespace UIView.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UIModel.API.Dto;

    public interface IPrimaryStatTableViewModelBindingHelper
    {
        void Rebind(ObservableCollection<IPrimaryStatViewModel> currentCollection, IEnumerable<UiPrimaryStat> statUpdate);
    }
}
