using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SubmitNewHaulierCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateHaulierViewModel _haulier;
        private readonly ViewModelBase _returnViewModel;
        private HaulierRecord haulierRecord;

        public SubmitNewHaulierCommand(NavigationStore navigationStore, CreateHaulierViewModel haulier, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._haulier = haulier;
            this._returnViewModel = returnViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //bool isHaulierValid = HaulierService.TryValidateHaulier(haulierRecord, out string[] feedback);
            bool isHaulierValid = true;
            return isHaulierValid && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            HaulierRecord _haulier = new HaulierRecord()
            {
                HaulierName = this._haulier.HaulierName,
                HaulierAddressLine1 = this._haulier.AddressLine1,
                HaulierAddressLine2 = this._haulier.AddressLine2,
                City = this._haulier.City,
                PostCode = this._haulier.PostCode,
                CountryId = this._haulier.CountryId
            };

            Haulier newHaulier = HaulierService.CreateHaulier(_haulier, out string[] feedback);

            HaulierViewModel haulier = HaulierService.GetHaulier(h => h.HaulierId == newHaulier.HaulierId, h => new HaulierViewModel()
            {
                HaulierId = h.HaulierId,
                HaulierName = h.Name,
                CountryId = h.Country.CountryId,
                CountryName = h.Country.Name
            });

            ((HaulierListingViewModel)_returnViewModel).AddHaulier(haulier);

            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
