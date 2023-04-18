using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    class SelectVehicleCommand : CommandBase
    {
        private readonly NavigationStore _navigatorStore;
        private readonly VehicleListingViewModel _selectedVehicle;
        private readonly ViewModelBase _returnViewModel;

        public SelectVehicleCommand(NavigationStore navigatorStore, VehicleListingViewModel selectedVehicle, ViewModelBase returnViewModel)
        {
            this._navigatorStore = navigatorStore;
            this._selectedVehicle = selectedVehicle;
            this._returnViewModel = returnViewModel;
        }


        public override void Execute(object parameter)
        {
            ((CreateBookingViewModel)_returnViewModel).VehicleId = _selectedVehicle.SelectedVehicle.VehicleId;
            _navigatorStore.CurrentViewModel = _returnViewModel;
        }
    }
}
