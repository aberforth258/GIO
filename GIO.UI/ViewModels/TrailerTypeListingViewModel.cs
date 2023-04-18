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
    public class TrailerTypeListingViewModel : ViewModelBase
    {
        ObservableCollection<TrailerTypeViewModel> _trailerTypes;

        public IEnumerable<TrailerTypeViewModel> TrailerTypes => _trailerTypes;

        private TrailerTypeViewModel _selectedTrailerType;
        public TrailerTypeViewModel SelectedTrailerType
        {
            get
            {
                return _selectedTrailerType;
            }
            set
            {
                _selectedTrailerType = value;
                OnPropertyChanged(nameof(SelectedTrailerType));
            }
        }

        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        public ICommand SelectTrailerTypeCommand { get; }
        public ICommand CancelSelectTrailerTypeCommand { get; }
        public ICommand CreateNewTrailerTypeCommand { get; }

        public TrailerTypeListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            _trailerTypes = new ObservableCollection<TrailerTypeViewModel>();
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SelectTrailerTypeCommand = new SelectTrailerTypeCommand(navigationStore,this, returnViewModel);
            CancelSelectTrailerTypeCommand = new NavigateCommand(navigationStore, returnViewModel);
            CreateNewTrailerTypeCommand = new NavigateCommand(navigationStore, new CreateTrailerTypeViewModel(navigationStore, this));

            this.UpdateTrailerTypes();
    }

        internal void AddTrailerType(TrailerTypeViewModel trailerType)
        {
            _trailerTypes.Add(trailerType);
        }

        internal void UpdateTrailerTypes()
        {
            _trailerTypes.Clear();

            foreach(TrailerTypeViewModel tt in TrailerTypeService.GetTrailerTypes(tt => true, tt => new TrailerTypeViewModel() 
            {
                TrailerTypeId = tt.TrailerTypeId,
                TrailerTypeName = tt.Name,
                TrailerTypeHeight = tt.Height
            }))
            {
                _trailerTypes.Add(tt);
            }
                
        }
    }
}
