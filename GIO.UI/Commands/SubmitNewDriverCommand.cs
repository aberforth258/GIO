using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SubmitNewDriverCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateDriverViewModel _driver;
        private readonly ViewModelBase _returnViewModel;
        private DriverRecord driverRecord;
        public SubmitNewDriverCommand(NavigationStore navigationStore, CreateDriverViewModel driver, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._driver = driver;
            this._returnViewModel = returnViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //bool isDriverValid = DriverService.TryValidateDriver(driverRecord, out string[] feedback);
            bool isDriverValid = true;
            return isDriverValid && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            DriverRecord driverRecord = new DriverRecord()
            {
                DriverName = this._driver.DriverName,
                ManagerName = this._driver.ManagerName,
                Phone = this._driver.Phone,
                HaulierId = this._driver.HaulierId,
                RequiresValidation = true
            };

            Driver newDriver = DriverService.CreateDriver(driverRecord, out string[] feedback);

            DriverViewModel driver = DriverService.GetDriver(d => d.DriverId == newDriver.DriverId, d => new DriverViewModel()
            {
                DriverId = d.DriverId,
                DriverName = d.Name,
                HaulierName = d.Haulier.Name
            });

            ((DriverListingViewModel)_returnViewModel).AddDriver(driver);
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
