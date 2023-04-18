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
    public class CreateTrailerViewModel : ViewModelBase
    {
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

        private long _trailerTypeId;
        public long TrailerTypeId
        {
            get
            {
                return  _trailerTypeId;
            }
            set
            {
                 _trailerTypeId = value;
                TrailerTypeName = TrailerTypeService.GetTrailerTypeName(_trailerTypeId);
                OnPropertyChanged(nameof(TrailerTypeId));
            }
        }

        private string _trailerTypeName;
        public string TrailerTypeName
        {
            get
            {
                return _trailerTypeName;
            }
            set
            {
                _trailerTypeName = value;
                OnPropertyChanged(nameof(TrailerTypeName));
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


        private readonly NavigationStore _navigatorStore;
        private readonly ViewModelBase _returnViewModel;

        public ICommand SubmitNewTrailerCommand { get; }
        public ICommand CancelSubmitNewTrailerCommand { get; }

        public ICommand TrailerTypeLookupCommand { get; }
        public ICommand HaulierLookupCommand { get; }


        public CreateTrailerViewModel(NavigationStore navigatorStore, ViewModelBase returnViewModel)
        {
            this._navigatorStore = navigatorStore;
            this._returnViewModel = returnViewModel;

            SubmitNewTrailerCommand = new SubmitNewTrailerCommand(_navigatorStore, this, _returnViewModel);
            CancelSubmitNewTrailerCommand = new NavigateCommand(_navigatorStore, _returnViewModel);
            TrailerTypeLookupCommand = new NavigateCommand(_navigatorStore, new TrailerTypeListingViewModel(navigatorStore, this) );
            HaulierLookupCommand = new NavigateCommand(_navigatorStore, new HaulierListingViewModel(navigatorStore, this));

        }
    }
}
