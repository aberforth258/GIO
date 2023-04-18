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
    public class CountryListingViewModel : ViewModelBase
    {
        ObservableCollection<CountryViewModel> _countries;

        private CountryViewModel _selectedCountry;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        public CountryViewModel SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public IEnumerable<CountryViewModel> Countries => _countries;

        public ICommand SelectCountryCommand { get; }
        public ICommand CancelSelectCountryCommand { get; }
        public ICommand CreateCountryCommand { get; }

        public CountryListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            _countries = new ObservableCollection<CountryViewModel>();
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SelectCountryCommand = new SelectCountryCommand(_navigationStore, this, _returnViewModel);
            CancelSelectCountryCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            CreateCountryCommand = new NavigateCommand(_navigationStore, new CreateCountryViewModel(_navigationStore, this));

            this.UpdateCountries();
        }

        internal void AddCountry(CountryViewModel country)
        {
            _countries.Add(country);
        }

        public void UpdateCountries()
        {
            _countries.Clear();

            foreach(CountryViewModel c in CountryService.GetCountries(c => true, c => new CountryViewModel()
            {
                CountryId = c.CountryId,
                CountryName = c.Name,
                CountryCode2 = c.CountryCode2,
                CountryCode3 = c.CountryCode3
            }))
            {
                _countries.Add(c);
            }
        }
    }
}
