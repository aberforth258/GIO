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
    class VehicleListingViewModel : ViewModelBase
    {
        private ObservableCollection<VehicleViewModel> _vehicles;
        private readonly NavigationStore navigationStore;
        private readonly ViewModelBase returnViewModel;


        private VehicleViewModel _selectedVehicle;
        public VehicleViewModel SelectedVehicle
        {
            get
            {
                return _selectedVehicle;
            }
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged(nameof( VehicleViewModel));
            }
        }

        public IEnumerable<VehicleViewModel> Vehicles => _vehicles;

        public ICommand SelectVehicleCommand { get; }
        public ICommand CreateVehicleCommand { get; }
        public ICommand CancelSelectVehicleCommand { get; }

        public VehicleListingViewModel( NavigationStore navigationStore,  ViewModelBase returnViewModel)
        {
            _vehicles = new ObservableCollection<VehicleViewModel>();
            this.navigationStore = navigationStore;
            this.returnViewModel = returnViewModel;

            var vehicles = VehicleService.GetVehicles<VehicleViewModel>(v => true, v => new VehicleViewModel()
            {
                VehicleId = v.VehicleId,
                VehicleRegPlate = v.RegPlate,
                IsBanned = v.IsBanned
            });

            foreach(VehicleViewModel v in vehicles)
            {
                _vehicles.Add(v);
            }

            navigationStore.CurrentViewModel = this;
            SelectVehicleCommand = new SelectVehicleCommand(navigationStore, this, returnViewModel);
            CreateVehicleCommand = new NavigateCommand(navigationStore, new CreateVehicleViewModel(navigationStore, this));
            CancelSelectVehicleCommand = new NavigateCommand(navigationStore, returnViewModel);
        }

        internal void AddVehicle(VehicleViewModel vehicle)
        {
            _vehicles.Add(vehicle);
        }

        internal void UpdateVehicles()
        {
            _vehicles.Clear();

            foreach(VehicleViewModel v in VehicleService.GetVehicles(v => true, v => new VehicleViewModel()
            {
                VehicleId = v.VehicleId,
                VehicleRegPlate = v.RegPlate,
                IsBanned = v.IsBanned
            }))
            {
                _vehicles.Add(v);
            }
        }
    }
}
