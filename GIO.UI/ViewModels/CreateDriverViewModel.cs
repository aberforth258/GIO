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
    public class CreateDriverViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

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
            }
        }
        private string _managerName;
        public string ManagerName
        {
            get
            {
                return _managerName;
            }
            set
            {
                _managerName = value;
                OnPropertyChanged(nameof(ManagerName));
            }
        }
        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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
                HaulierName = HaulierService.GetHaulierName(_haulierId);
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
            }
        }

        public ICommand SubmitNewDriverCommand { get; }
        public ICommand CancelSubmitNewDriverCommand { get; }

        public ICommand HaulierLookupCommand { get; }

        public CreateDriverViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewDriverCommand = new SubmitNewDriverCommand(_navigationStore, this, _returnViewModel);
            CancelSubmitNewDriverCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            HaulierLookupCommand = new NavigateCommand(_navigationStore, new HaulierListingViewModel(_navigationStore, this));
        }

    }
}
