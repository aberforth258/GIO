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
    public class CreateCountryViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

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
        private string _countryCode2;
        public string CountryCode2
        {
            get
            {
                return _countryCode2;
            }
            set
            {
                _countryCode2 = value;
                OnPropertyChanged(nameof(CountryCode2));
            }
        }
        private string _countryCode3;
        public string CountryCode3
        {
            get
            {
                return _countryCode3;
            }
            set
            {
                _countryCode3 = value;
                OnPropertyChanged(nameof(CountryCode3));
            }
        }

        public ICommand SubmitNewCountryCommand { get; }
        public ICommand CancelNewCountryCommand { get; }

        public CreateCountryViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewCountryCommand = new SubmitNewCountryCommand(_navigationStore, this, returnViewModel);
            CancelNewCountryCommand = new NavigateCommand(_navigationStore, _returnViewModel);
        }

    }
}
