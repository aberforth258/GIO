using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SelectVehicleTypeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VehicleTypeListingViewModel _selectedVehicleType;
        private readonly ViewModelBase _returnViewModel;

        public SelectVehicleTypeCommand(NavigationStore navigationStore, VehicleTypeListingViewModel selectedVehicleType, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedVehicleType = selectedVehicleType;
            this._returnViewModel = returnViewModel;
        }
        public override void Execute(object parameter)
        {
            ((CreateVehicleViewModel)_returnViewModel).VehicleTypeId = _selectedVehicleType.SelectedVehicleType.VehicleTypeId;
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
