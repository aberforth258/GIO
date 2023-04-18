using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    class SelectDriverCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        private readonly DriverListingViewModel _selectedDriver;
        
        public SelectDriverCommand(NavigationStore navigationStore, DriverListingViewModel selectedDriver, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedDriver = selectedDriver;
            this._returnViewModel = returnViewModel;
        }

        public override void Execute(object parameter)
        {
            ((CreateBookingViewModel)_returnViewModel).DriverId = _selectedDriver.SelectedDriver.DriverId;
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
