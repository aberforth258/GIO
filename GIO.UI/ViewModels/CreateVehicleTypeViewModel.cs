using GIO.UI.Commands;
using GIO.UI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GIO.UI.ViewModels
{
    public class CreateVehicleTypeViewModel : ViewModelBase
    {
        private string _vehicleTypeName;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        public string VehicleTypeName
        {
            get
            {
                return _vehicleTypeName;
            }
            set
            {
                _vehicleTypeName = value;
                OnPropertyChanged(nameof(VehicleTypeName));
            }
        }

         public ICommand SubmitNewVehicleType { get; }
         public ICommand CancelNewVehicleType { get; }

        public CreateVehicleTypeViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewVehicleType = new SubmitNewVehicleType(_navigationStore, this, _returnViewModel);
            CancelNewVehicleType = new NavigateCommand(_navigationStore, _returnViewModel);
        }
    }

    
}
