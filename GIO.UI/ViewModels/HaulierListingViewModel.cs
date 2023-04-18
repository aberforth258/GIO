using GIO.Interfaces;
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
    public class HaulierListingViewModel : ViewModelBase
    {
        ObservableCollection<HaulierViewModel> _hauliers;

        public IEnumerable<HaulierViewModel> Hauliers => _hauliers;

        private HaulierViewModel _selectedHaulier;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        public HaulierViewModel SelectedHaulier
        {
            get
            {
                return _selectedHaulier;
            }
            set
            {
                _selectedHaulier = value;
                OnPropertyChanged(nameof(SelectedHaulier));
            }
        }

        public ICommand SelectHaulierCommand { get; }
        public ICommand CancelSelectHaulierCommand { get; }
        public ICommand CreateHaulierCommand { get; }
        

        public HaulierListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            _hauliers = new ObservableCollection<HaulierViewModel>();
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            var hauliers = HaulierService.GetHauliers(h => true, h => new HaulierViewModel()
            {
                HaulierId = h.HaulierId,
                HaulierName = h.Name,
                CountryId = h.Country.CountryId,
                CountryName = h.Country.Name
            });

            foreach(HaulierViewModel h in hauliers)
            {
                _hauliers.Add(h);
            }

            SelectHaulierCommand = new SelectHaulierCommand(_navigationStore, this, _returnViewModel);
            CancelSelectHaulierCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            CreateHaulierCommand = new NavigateCommand(_navigationStore, new CreateHaulierViewModel(_navigationStore, this));
            
        }

        internal void AddHaulier(HaulierViewModel haulier)
        {
            _hauliers.Add(haulier);
        }

        public void UpdateHauliers()
        {
            _hauliers.Clear();

            foreach (HaulierViewModel h in HaulierService.GetHauliers(h => true, h => new HaulierViewModel()
            {
                HaulierId = h.HaulierId,
                HaulierName = h.Name,
                CountryId = h.Country.CountryId,
                CountryName = h.Country.CountryCode3
            }))
            {
                _hauliers.Add(h);
            }
        }
    }
}
