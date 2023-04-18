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
    public class CreateHaulierViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;


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

        private string _addressLine1;
        public string AddressLine1
        {
            get
            {
                return _addressLine1;
            }
            set
            {
                _addressLine1 = value;
                OnPropertyChanged(nameof(AddressLine1));
            }
        }
        private string _addressLine2;
        public string AddressLine2
        {
            get
            {
                return _addressLine2;
            }
            set
            {
                _addressLine2 = value;
                OnPropertyChanged(nameof(AddressLine2));
            }
        }
        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }


        private string _postCode;
        public string PostCode
        {
            get
            {
                return _postCode;
            }
            set
            {
                _postCode = value;
                OnPropertyChanged(nameof(PostCode));
            }
        }
        private long _countryId;
        public long CountryId
        {
            get
            {
                return _countryId;
            }
            set
            {
                _countryId = value;
                _countryName = CountryService.GetCountry(_countryId).Name;
                OnPropertyChanged(nameof(CountryId));
            }
        }
        private string _countryName;
        public string CountryName
        {
            get
            {
                return _countryName;
            }
            set
            {
                _countryName = value;
                OnPropertyChanged(nameof(CountryName));
            }
        }

        public ICommand SubmitNewHaulierCommand { get; }
        public ICommand CancelNewHaulierCommand { get; }
        public ICommand SelectCountryCommand { get; }

        public CreateHaulierViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewHaulierCommand = new SubmitNewHaulierCommand(_navigationStore, this, _returnViewModel);
            CancelNewHaulierCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            SelectCountryCommand = new NavigateCommand(_navigationStore, new CountryListingViewModel(_navigationStore, this));
        }
    }
}
