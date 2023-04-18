using GIO.Services;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    class DeleteBookingsCommand : CommandBase
    {
        private readonly ViewModelBase _viewModel;
        private BookingViewModel bookingViewModel;
        public DeleteBookingsCommand(ViewModelBase viewModel)
        {
            this._viewModel = viewModel;

            ((BookingListingViewModel)_viewModel).PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bookingViewModel = ((BookingListingViewModel)_viewModel).SelectedBooking;
            OnCanExecutedChanged();
        }

        public override bool CanExecute(object parameter)
        {
            //Is collection empty

            bool isBookingEmpty = bookingViewModel is null;
            
            return false == isBookingEmpty &&  base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            BookingService.TryDeleteBooking(bookingViewModel.BookingId, out string feedback);
            ((BookingListingViewModel)_viewModel).RefreshBookingList();
        }
    }
}
