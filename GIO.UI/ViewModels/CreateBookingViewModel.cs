using GIO.UI.Commands;
using GIO.Interfaces;
using GIO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GIO.UI.Stores;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace GIO.UI.ViewModels
{
    public class CreateBookingViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        /****************************************************************/
        /*******       PROPERTIES                     ******************/
        /****************************************************************/
        private string _customerReference;
        public string CustomerReference
        {
            get
            {
                return _customerReference;
            }
            set
           {
                _customerReference = value;
                OnPropertyChanged(nameof(CustomerReference));

                ClearErrors(nameof(CustomerReference));

                if(string.IsNullOrEmpty(_customerReference))
                {
                    AddError(nameof(CustomerReference), "Customer reference cannot be empty");
                }

            }
        }

        private DateTime _windowStart;
        public DateTime WindowStart
        {
            get
            {
                return _windowStart;
            }
            set
            {
                _windowStart = value;
                OnPropertyChanged(nameof(WindowStart));

                ClearErrors(nameof(WindowStart));
                ClearErrors(nameof(WindowEnd));

                if(WindowStart >= WindowEnd)
                {
                    AddError(nameof(WindowStart), "Start date cannot be later than window end");
                }
            }
        }

        private DateTime _windowEnd;
        public DateTime WindowEnd
        {
            get
            {
                return _windowEnd;
            }
            set
            {
                _windowEnd = value;
                OnPropertyChanged(nameof(WindowEnd));

                ClearErrors(nameof(WindowStart));
                ClearErrors(nameof(WindowEnd));

                if (WindowStart >= WindowEnd)
                {
                    AddError(nameof(WindowEnd), "End date cannot be earlier than window start");
                }
            }
        }

        private long _driverId;
        public long DriverId
        {
            get
            {
                return _driverId;
            }
            set
            {
                _driverId = value;
                DriverName = DriverService.GetDriverName(DriverId);
                OnPropertyChanged(nameof(DriverId));

            }
        }

        private string _driverName;
        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(DriverName));

                ClearErrors(nameof(DriverName));

                if (string.IsNullOrEmpty(DriverName))
                {
                    AddError(nameof(DriverName), "Driver must be set");
                }
            }
        }

        private long _vehicleId;
        public long VehicleId
        {
            get
            {
                return _vehicleId;
            }
            set
            {
                _vehicleId = value;
                VehicleRegPlate = VehicleService.GetVehicleRegPlate(VehicleId);
                OnPropertyChanged(nameof(VehicleId));
            }
        }

        private string _vehicleRegPlate;
        public string VehicleRegPlate
        {
            get
            {
                return _vehicleRegPlate;
            }
            set
            {
                _vehicleRegPlate = value;
                OnPropertyChanged(nameof(VehicleRegPlate));

                ClearErrors(nameof(VehicleRegPlate));

                if (string.IsNullOrEmpty(VehicleRegPlate))
                {
                    AddError(nameof(DriverId), "Driver must be set");
                }
            }
        }

        private long? _trailerId;
        public long? TrailerId
        {
            get
            {
                return _trailerId;
            }
            set
            {
                _trailerId = value;
                TrailerName = TrailerService.GetTrailerName((long)TrailerId);
                OnPropertyChanged(nameof(TrailerId));
            }
        }

        private string _trailerName;
        public string TrailerName
        {
            get
            {
                return _trailerName;
            }
            set
            {
                _trailerName = value;
                OnPropertyChanged(nameof(TrailerName));
            }
        }

        private long _haulierId;
        public long HaulierId
        {
            get
            {
                return _haulierId;
            }
            set
            {
                _haulierId = value;
                HaulierName = HaulierService.GetHaulierName(HaulierId);
                OnPropertyChanged(nameof(HaulierId));
            }
        }

        private string _haulierName;
        public string HaulierName
        {
            get
            {
                return _haulierName;
            }
            set
            {
                _haulierName = value;
                OnPropertyChanged(nameof(HaulierName));

                ClearErrors(nameof(HaulierName));

                if(string.IsNullOrEmpty(HaulierName))
                {
                    AddError(nameof(HaulierName), "Haulier must be set");
                }
            }
        }

        private readonly Dictionary<string, List<String>> _propertyNameToErrorsDictionary;
        private bool hasErrors;
        public bool HasErrors
        {
            get
            {
                return hasErrors;
            }
            set
            {
                hasErrors = value;
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private ObservableCollection<string> errorMessages;

        public IEnumerable<string> ErrorMessages => errorMessages;

        /****************************************************************/
        /*******       COMMANDS                        ******************/
        /****************************************************************/

        public ICommand SubmitCreateBookingCommand { get; }
        public ICommand CancelCreateBookingCommand { get; }

        public ICommand SelectDriverCommand { get; }
        public ICommand SelectVehicleCommand { get; }
        public ICommand SelectTrailerCommand { get; }
        public ICommand SelectHaulierCommand { get; }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="returnViewModel">View model page should return to</param>
        public CreateBookingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            SubmitCreateBookingCommand = new SubmitNewBookingCommand(navigationStore, this, returnViewModel);


            SelectDriverCommand = new NavigateCommand(navigationStore, new DriverListingViewModel(navigationStore, this));
            SelectVehicleCommand = new NavigateCommand(navigationStore, new VehicleListingViewModel(navigationStore, this));
            SelectTrailerCommand = new NavigateCommand(navigationStore, new TrailerListingViewModel(navigationStore, this));
            SelectHaulierCommand = new NavigateCommand(navigationStore, new HaulierListingViewModel(navigationStore, this));

            CancelCreateBookingCommand = new NavigateCommand(navigationStore, returnViewModel);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            
            errorMessages = new ObservableCollection<string>();

            WindowStart = DateTime.Now;
            WindowEnd = DateTime.Now.AddDays(1);

            
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

            UpdateErrorListForUI();
            HasErrors = errorMessages.Any();
        }

        private void UpdateErrorListForUI()
        {
            
            errorMessages.Clear();
            foreach(List<string> eList in _propertyNameToErrorsDictionary.Values)
            {
                foreach (string e in eList)
                { 
                  errorMessages.Add(e);
                }

            }
            

            //errorMessages.Clear();
            //foreach( string e in ((SubmitNewBookingCommand)SubmitCreateBookingCommand).ValidationErrors.ToList() )
            //{
            //    errorMessages.Add(e);
            //}
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }
    }
}
