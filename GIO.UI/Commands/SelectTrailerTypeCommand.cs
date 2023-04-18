using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SelectTrailerTypeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly TrailerTypeListingViewModel _selectedTrailerType;
        private readonly ViewModelBase _returnViewModel;

        public SelectTrailerTypeCommand(NavigationStore navigationStore, TrailerTypeListingViewModel selectedTrailerType, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedTrailerType = selectedTrailerType;
            this._returnViewModel = returnViewModel;
        }

        public override void Execute(object parameter)
        {
            ((CreateTrailerViewModel)_returnViewModel).TrailerTypeId = this._selectedTrailerType.SelectedTrailerType.TrailerTypeId;
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
