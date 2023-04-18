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
    public class SubmitNewTrailerTypeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateTrailerTypeViewModel _trailerType;
        private readonly ViewModelBase _returnViewModel;
        private TrailerTypeRecord trailerTypeRecord;

        public SubmitNewTrailerTypeCommand(NavigationStore navigationStore, CreateTrailerTypeViewModel trailerType, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._trailerType = trailerType;
            this._returnViewModel = returnViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //bool isTrailerTypeValid = TrailerTypeService.TryValidateTrailerType(trailerTypeRecord, out string[] feedback);
            bool isTrailerTypeValid = true;
            return isTrailerTypeValid && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TrailerTypeRecord trailerTypeRecord = new TrailerTypeRecord()
            {
                TrailerTypeName = this._trailerType.TrailerTypeName,
                Height = this._trailerType.Height
            };

            
            TrailerType newTrailerType = TrailerTypeService.CreateTrailerType(trailerTypeRecord, out string[] feedback);

            TrailerTypeViewModel trailerType = TrailerTypeService.GetTrailerType(t => t.TrailerTypeId == newTrailerType.TrailerTypeId, t => new TrailerTypeViewModel()
            {
                TrailerTypeId = t.TrailerTypeId,
                TrailerTypeName = t.Name,
                TrailerTypeHeight = t.Height
            });

            ((TrailerTypeListingViewModel)_returnViewModel).AddTrailerType(trailerType);
            _navigationStore.CurrentViewModel = this._returnViewModel;
        }
    }
}
