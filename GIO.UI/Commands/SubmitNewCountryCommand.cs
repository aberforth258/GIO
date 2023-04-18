using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SubmitNewCountryCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateCountryViewModel _country;
        private readonly ViewModelBase _returnViewModel;
        private CountryRecord countryRecord;
        public SubmitNewCountryCommand(NavigationStore navigationStore, CreateCountryViewModel country, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._country = country;
            this._returnViewModel = returnViewModel;

            country.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            countryRecord = new CountryRecord()
            {
                CountryName = _country.CountryName,
                CountryCode2 = _country.CountryCode2,
                CountryCode3 = _country.CountryCode3
            };
        }

        public override bool CanExecute(object parameter)
        {
            //bool isCountryValid = CountryService.TryValidateCountry(countryRecord, out string[] feedback);
            bool isCountryValid = true;
            return isCountryValid && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            CountryRecord countryRecord = new CountryRecord()
            {
                CountryName = _country.CountryName,
                CountryCode2 = _country.CountryCode2,
                CountryCode3 = _country.CountryCode3
            };

            Country newCountry = CountryService.CreateCountry(countryRecord, out string[] feedback);

            CountryViewModel country = CountryService.GetCountry(c => c.CountryId == newCountry.CountryId, c => new CountryViewModel()
            {
                CountryId = c.CountryId,
                CountryName = c.Name,
                CountryCode2 = c.CountryCode2,
                CountryCode3 = c.CountryCode3

            });

            ((CountryListingViewModel)_returnViewModel).AddCountry(country);

            _navigationStore.CurrentViewModel = _returnViewModel;


        }
    }
}
