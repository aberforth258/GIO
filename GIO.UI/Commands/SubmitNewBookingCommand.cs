using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    public class SubmitNewBookingCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateBookingViewModel _createBookingViewModel;
        private readonly ViewModelBase _returnViewModel;
        private BookingRecord _validateBookingRecord;

        public SubmitNewBookingCommand(NavigationStore navigationStore, CreateBookingViewModel createBookingViewModel, ViewModelBase returnViewModel)
        {
            this._navigationStore = navigationStore;
            this._createBookingViewModel = createBookingViewModel;
            this._returnViewModel = returnViewModel;

            createBookingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this._validateBookingRecord = new BookingRecord()
            {
                CustomerReference = this._createBookingViewModel.CustomerReference,
                WindowStart = this._createBookingViewModel.WindowStart,
                WindowEnd = this._createBookingViewModel.WindowEnd,
                DriverId = this._createBookingViewModel.DriverId,
                VehicleId = this._createBookingViewModel.VehicleId,
                TrailerId = this._createBookingViewModel.TrailerId,
                HaulierId = this._createBookingViewModel.HaulierId
            };

            OnCanExecutedChanged();
            
        }

        public override bool CanExecute(object parameter)
        {
            bool isBookingValid = BookingService.TryValidateBooking(this._validateBookingRecord, out string[] feedback);
            return isBookingValid  && base.CanExecute(parameter);
        }

        public override void Execute(object parameter) 
        {
            BookingRecord bookingRecord = new BookingRecord()
            {
                CustomerReference = _createBookingViewModel.CustomerReference,
                WindowStart = _createBookingViewModel.WindowStart,
                WindowEnd = _createBookingViewModel.WindowEnd,
                DriverId = _createBookingViewModel.DriverId,
                VehicleId = _createBookingViewModel.VehicleId,
                TrailerId = _createBookingViewModel.TrailerId,
                HaulierId = _createBookingViewModel.HaulierId,
                RequiresValidation = true
            };

            Booking newBooking = BookingService.CreateEditBooking(bookingRecord, out string[] feedback, false);

            BookingViewModel booking = BookingService.GetBooking(b => b.BookingId == newBooking.BookingId, b => new BookingViewModel()
            {
                CustomerReference = b.CustomerRef,
                WindowStart = b.BookingWindowFrom,
                WindowEnd = b.BookingWindowTo,
                DriverName = b.Driver.Name,
                VehicleRegPlate = b.Vehicle.RegPlate,
                TrailerName = b.Trailer.Name,
                HaulierName = b.Haulier.Name,
                Status = b.BookingStatus.Name
            });

            ((BookingListingViewModel)_returnViewModel).AddBooking(booking);
            _navigationStore.CurrentViewModel = _returnViewModel;
        }
    }
}
