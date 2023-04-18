using GIO.Services;
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
    public class CreateVehicleViewModel : ViewModelBase
    {
        private string _regPlate;
        public string RegPlate
        {
            get
            {
                return _regPlate;
            }
            set
            {
                _regPlate = value;
                OnPropertyChanged(nameof(RegPlate));
            }
        }
        private bool _isBanned;
        public bool IsBanned
        {
            get
            {
                return _isBanned;
            }
            set
            {
                _isBanned = value;
                OnPropertyChanged(nameof(IsBanned));
            }
        }

        private long _vehicleTypeId;
        public long VehicleTypeId
        {
            get
            {
                return _vehicleTypeId;
            }
            set
            {
                _vehicleTypeId = value;
                VehicleTypeName = VehicleTypeService.GetVehicleTypeName(_vehicleTypeId);
                OnPropertyChanged(nameof(VehicleTypeId));
            }
        }

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

        public ICommand SubmitNewVehicleCommand { get; }
        public ICommand CancelNewVehicleCommand { get; }
        public ICommand VehicleTypeLookupCommand { get; }

        public CreateVehicleViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewVehicleCommand = new SubmitNewVehicleCommand(_navigationStore, this, _returnViewModel);
            CancelNewVehicleCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            VehicleTypeLookupCommand = new NavigateCommand(_navigationStore, new VehicleTypeListingViewModel(_navigationStore, this));
        }
    }
}
