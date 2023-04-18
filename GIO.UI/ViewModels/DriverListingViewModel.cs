using GIO.Services;
using GIO.UI.Commands;
using GIO.UI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GIO.UI.ViewModels
{
    class DriverListingViewModel :ViewModelBase
    {
        private readonly ObservableCollection<DriverViewModel> _drivers;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;
        private DriverViewModel _selectedDriver;
        public DriverViewModel SelectedDriver
        {
            get
            {
                return _selectedDriver;
            }
            set
            {
                _selectedDriver = value;
                OnPropertyChanged(nameof(SelectedDriver));
            }
        }

        public IEnumerable<DriverViewModel> Drivers => _drivers;



        public ICommand SelectDriverCommand { get; }
        public ICommand CreateDriverCommand { get; }
        public ICommand CancelSelectDriverCommand { get; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="returnViewModel">View model the page should return to</param>
        public DriverListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            _drivers = new ObservableCollection<DriverViewModel>();
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            this.UpdateDrivers();

            navigationStore.CurrentViewModel = this;
            SelectDriverCommand = new SelectDriverCommand(navigationStore, this, returnViewModel);
            CancelSelectDriverCommand = new NavigateCommand(navigationStore, returnViewModel);
            CreateDriverCommand = new NavigateCommand(navigationStore, new CreateDriverViewModel(navigationStore, this));
        }

        internal void AddDriver(DriverViewModel driver)
        {
            _drivers.Add(driver);
        }

        public void UpdateDrivers()
        {
            _drivers.Clear();

            foreach(DriverViewModel d in DriverService.GetDrivers(d => true, d => new DriverViewModel()
            {
                DriverId = d.DriverId,
                DriverName = d.Name,
                HaulierName = d.Haulier.Name
            }))
            {
                _drivers.Add(d);
            }
        }
    }
}
