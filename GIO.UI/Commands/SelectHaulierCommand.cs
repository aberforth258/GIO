using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SelectHaulierCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly HaulierListingViewModel _selectedHaulier;
        private readonly ViewModelBase _returnViewModel;

        public SelectHaulierCommand(NavigationStore navigationStore, HaulierListingViewModel selectedHaulier, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._selectedHaulier = selectedHaulier;
            this._returnViewModel = returnViewModel;
        }

        public override void Execute(object parameter)
        {
            if( _returnViewModel is CreateBookingViewModel)
            {
                ((CreateBookingViewModel)_returnViewModel).HaulierId = _selectedHaulier.SelectedHaulier.HaulierId;
            }
            else if( _returnViewModel is CreateDriverViewModel)
            {
                ((CreateDriverViewModel)_returnViewModel).HaulierId = _selectedHaulier.SelectedHaulier.HaulierId;
            }
            else if(_returnViewModel is CreateTrailerViewModel)
            {
                ((CreateTrailerViewModel)_returnViewModel).HaulierId = _selectedHaulier.SelectedHaulier.HaulierId;
            }
            else
            {
                throw new Exception(@"Incorrect data view");
            }
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
