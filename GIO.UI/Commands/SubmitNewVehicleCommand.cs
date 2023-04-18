using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SubmitNewVehicleCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateVehicleViewModel _vehicle;
        private readonly ViewModelBase _returnViewModel;
        private VehicleRecord vehicleRecord;
        public SubmitNewVehicleCommand(NavigationStore navigationStore, CreateVehicleViewModel vehicle, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._vehicle = vehicle;
            this._returnViewModel = returnViewModel;

            vehicle.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            vehicleRecord = new VehicleRecord()
            {
                RegPlate = _vehicle.RegPlate,
                IsBanned = _vehicle.IsBanned,
                VehicleTypeId = _vehicle.VehicleTypeId
            };

            OnCanExecutedChanged();

        }

        public override bool CanExecute(object parameter)
        {
            //bool isVehicleValid = VehicleService.TryValidateVehicle(vehicleRecord, out string[] feedback);
            bool isVehicleValid = true;
            return isVehicleValid && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            VehicleRecord vehicleRecord = new VehicleRecord()
            {
                RegPlate = this._vehicle.RegPlate,
                IsBanned = this._vehicle.IsBanned,
                VehicleTypeId = this._vehicle.VehicleTypeId
            };

            Vehicle newVehicle = VehicleService.CreateVehicle(vehicleRecord, out string[] feedback);

            VehicleViewModel vehicle = VehicleService.GetVehicle(v => v.VehicleId == newVehicle.VehicleId, v => new VehicleViewModel()
            {
                VehicleId = v.VehicleId,
                VehicleRegPlate = v.RegPlate,
                IsBanned = v.IsBanned
            });

            ((VehicleListingViewModel)_returnViewModel).AddVehicle(vehicle);
            _navigationStore.CurrentViewModel = this._returnViewModel;

        }
    }
}
