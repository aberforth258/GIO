using GIO.UI.Stores;
using GIO.UI.ViewModels;
using GIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIO.Services;
using GIO.Models;

namespace GIO.UI.Commands
{
    public class SubmitNewVehicleType : CommandBase
    {
        private NavigationStore _navigationStore;
        private CreateVehicleTypeViewModel _vehicleType;
        private ViewModelBase _returnViewModel;
        private VehicleTypeRecord vehicleTypeRecord;

        public SubmitNewVehicleType(NavigationStore navigationStore, CreateVehicleTypeViewModel vehicleType, ViewModelBase returnViewModel)
        {
            _navigationStore = navigationStore;
            this._vehicleType = vehicleType;
            _returnViewModel = returnViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //bool isVehicleTypeValid = VehicleTypeService.TryValidateVehicleType(vehicleTypeRecord, out string[] feedback);
            bool isVehicleTypeValid = true;
            return isVehicleTypeValid && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            VehicleTypeRecord vehicleTypeRecord = new VehicleTypeRecord()
            {
                VehicleTypeName = _vehicleType.VehicleTypeName,
                RequiresValidation = true

            };

            VehicleType newVehicleType = VehicleTypeService.CreateVehicleType(vehicleTypeRecord, out string[] feedback);

            VehicleTypeViewModel vehicleType = VehicleTypeService.GetVehicleType(vt => vt.VehicleTypeId == newVehicleType.VehicleTypeId, vt => new VehicleTypeViewModel()
            {

                VehicleTypeId = vt.VehicleTypeId,
                VehicleTypeName = vt.Name
            });


            ((VehicleTypeListingViewModel)_returnViewModel).AddVehicleType(vehicleType);
            _navigationStore.CurrentViewModel = this._returnViewModel;

        }
    }
}
