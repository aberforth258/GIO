using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SelectTrailerCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly TrailerListingViewModel _selectedViewModel;
        private readonly ViewModelBase _returnViewModel;

        public SelectTrailerCommand(NavigationStore navigationStore, TrailerListingViewModel selectedViewModel, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedViewModel = selectedViewModel;
            this._returnViewModel = returnViewModel;
        }
        public override void Execute(object parameter)
        {
            ((CreateBookingViewModel)_returnViewModel).TrailerId = _selectedViewModel.SelectedTrailer.TrailerId;
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
