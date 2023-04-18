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
    public class CreateTrailerTypeViewModel : ViewModelBase
    {
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

        private decimal _height;
        public decimal Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _returnViewModel;


        public ICommand SubmitNewTrailerTypeCommand { get; }
        public ICommand CancelNewTrailerTypeCommand { get; }

        public CreateTrailerTypeViewModel(NavigationStore navigationStore, ViewModelBase returnViewModel )
        {
            this._navigationStore = navigationStore;
            this._returnViewModel = returnViewModel;

            SubmitNewTrailerTypeCommand = new SubmitNewTrailerTypeCommand(navigationStore, this, returnViewModel);
            CancelNewTrailerTypeCommand = new NavigateCommand(navigationStore, returnViewModel);    
    }
    }
}
