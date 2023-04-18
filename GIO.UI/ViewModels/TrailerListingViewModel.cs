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
    public class TrailerListingViewModel : ViewModelBase
    {
        ObservableCollection<TrailerViewModel> _trailers;

        private TrailerViewModel _selectedTrailer;
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;

        public TrailerViewModel SelectedTrailer
        {
            get
            {
                return _selectedTrailer;
            }
            set
            {
                _selectedTrailer = value;
                OnPropertyChanged(nameof(SelectedTrailer));
            }
        }

        public IEnumerable<TrailerViewModel> Trailers => _trailers;

        public ICommand SelectTrailerCommand { get; }
        public ICommand CancelSelectTrailerCommand { get; }
        public ICommand CreateTrailerCommand { get; }

        public TrailerListingViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            _trailers = new ObservableCollection<TrailerViewModel>();

            SelectTrailerCommand = new SelectTrailerCommand(_navigationStore, this, _returnViewModel);
            CancelSelectTrailerCommand = new NavigateCommand(_navigationStore, _returnViewModel);
            CreateTrailerCommand = new NavigateCommand(_navigationStore, new CreateTrailerViewModel(navigationStore, this));

            this.UpdateTrailers();
        }

        internal void AddTrailer(TrailerViewModel trailer)
        {
            _trailers.Add(trailer);
        }

        public void UpdateTrailers()
        {
            _trailers.Clear();

            foreach(TrailerViewModel t in TrailerService.GetTrailers(t => true, t => new TrailerViewModel()
            {
                TrailerId = t.TrailerId,
                TrailerName = t.Name,
                HaulierName = t.Haulier.Name,
                TrailerType = t.TrailerType.Name
            }))
            {
                _trailers.Add(t);
            }
        }
    }
}
