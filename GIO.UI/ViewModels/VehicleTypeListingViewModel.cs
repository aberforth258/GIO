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
    public class VehicleTypeListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<VehicleTypeViewModel> _vehicleTypes;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        private VehicleTypeViewModel _selectedVehicleType;
        public VehicleTypeViewModel SelectedVehicleType
        {
            get
            {
                return _selectedVehicleType;
            }
            set
            {
                _selectedVehicleType = value;
                OnPropertyChanged(nameof(SelectedVehicleType));
            }
        }

        public IEnumerable<VehicleTypeViewModel> VehicleTypes => _vehicleTypes;

        public ICommand SelectVehicleTypeCommand { get; }
        public ICommand CancelSelectVehicleTypeCommand { get; }
        public ICommand CreateNewVehicleTypeCommand { get; }

        public VehicleTypeListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            _vehicleTypes = new ObservableCollection<VehicleTypeViewModel>();
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SelectVehicleTypeCommand = new SelectVehicleTypeCommand(_navigationStore, this, _returnViewModel);
            CancelSelectVehicleTypeCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            CreateNewVehicleTypeCommand = new NavigateCommand(_navigationStore, new CreateVehicleTypeViewModel(_navigationStore, this));

            this.UpdateVehicleTypes();
        }

        internal void AddVehicleType(VehicleTypeViewModel vehicleType)
        {
            _vehicleTypes.Add(vehicleType);
        }

        public void UpdateVehicleTypes()
        {
            this._vehicleTypes.Clear();

            foreach (VehicleTypeViewModel vt in VehicleTypeService.GetVehicleTypes(vt => true, vt => new VehicleTypeViewModel()
            {
                VehicleTypeId = vt.VehicleTypeId,
                VehicleTypeName = vt.Name
            }))
            {
                _vehicleTypes.Add(vt);
            }

        }
    }
}
