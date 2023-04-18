using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    class SelectCountryCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CountryListingViewModel _selectedCountry;
        private readonly ViewModelBase _returnViewModel;

        public SelectCountryCommand(NavigationStore navigationStore, CountryListingViewModel selectedCountry, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedCountry = selectedCountry;
            this._returnViewModel = returnViewModel;
        }
        public override void Execute(object parameter)
        {
            ((CreateHaulierViewModel)_returnViewModel).CountryId = _selectedCountry.SelectedCountry.CountryId;
            _navigationStore.CurrentViewModel = _returnViewModel;

        }
    }
}
