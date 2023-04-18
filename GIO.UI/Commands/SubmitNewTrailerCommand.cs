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
    public class SubmitNewTrailerCommand : CommandBase
    {
        private readonly NavigationStore _navigatorStore;
        private readonly CreateTrailerViewModel _trailer;
        private readonly ViewModelBase _returnViewModel;
        private TrailerRecord trailerRecord;

        public SubmitNewTrailerCommand(NavigationStore navigatorStore, CreateTrailerViewModel trailer, ViewModelBase returnViewModel)
        {
            _navigatorStore = navigatorStore;
            this._trailer = trailer;
            _returnViewModel = returnViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            //bool isTrailerValid = TrailerService.TryValidateTrailer(trailerRecord, out string[] feeedback);
            bool isTrailerValid = true;
            return isTrailerValid && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TrailerRecord trailerRecord = new TrailerRecord()
            {
                TrailerName = this._trailer.TrailerName,
                HaulierId = this._trailer.HaulierId,
                TrailerTypeId = this._trailer.TrailerTypeId,
                RequiresValidation = true
            };

            Trailer newTrailer = TrailerService.CreateTrailer(trailerRecord, out string[] feedback);

            TrailerViewModel trailer = TrailerService.GetTrailer(t => t.TrailerId == newTrailer.TrailerId, t => new TrailerViewModel()
            {
                TrailerId = t.TrailerId,
                TrailerName = t.Name,
                TrailerType = t.TrailerType.Name,
                HaulierName = t.Haulier.Name
            });

            ((TrailerListingViewModel)_returnViewModel).AddTrailer(trailer);
            _navigatorStore.CurrentViewModel = this._returnViewModel;

        }
    }
}
