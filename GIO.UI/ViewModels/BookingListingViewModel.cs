using GIO.Interfaces;
using GIO.Services;
using GIO.UI.Commands;
using GIO.UI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace GIO.UI.ViewModels
{
    public class BookingListingViewModel : ViewModelBase
    {

        private ObservableCollection<BookingViewModel> _bookings;
        public IEnumerable<BookingViewModel> Bookings => _bookings;

        private BookingViewModel selectedBooking;
        public BookingViewModel SelectedBooking
        {
            get
            {
                return selectedBooking;
            }
            set
            {
                selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }


        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand CreateBookingCommand { get; private set; }
        public ICommand DeleteBookingsCommand { get; }
        public BookingListingViewModel(NavigationStore navigationStore)
        {
            _bookings = new ObservableCollection<BookingViewModel>();
            
            RefreshBookingList();            

            CreateBookingCommand = new NavigateCommand(navigationStore, new CreateBookingViewModel(navigationStore, this));
            DeleteBookingsCommand = new DeleteBookingsCommand(this);
            _navigationStore = navigationStore;
        }

        public void AddBooking(BookingViewModel booking)
        {
            try
            {

                _bookings.Add(booking);
                OnPropertyChanged(nameof(_bookings));
                ClearForm();
            }
            catch
            {

            }
        }

        private void ClearForm()
        {
            CreateBookingCommand = new NavigateCommand(_navigationStore, new CreateBookingViewModel(_navigationStore, this));
        }

        public void RefreshBookingList()
        {
            var bookings = BookingService.GetBookings(b => true, b => new BookingViewModel()
            {
                BookingId = b.BookingId,
                CustomerReference = b.CustomerRef,
                Status = b.BookingStatus.Name,
                WindowStart = b.BookingWindowFrom,
                WindowEnd = b.BookingWindowTo,
                DriverName = b.Driver.Name,
                TrailerName = b.Trailer.Name,
                VehicleRegPlate = b.Vehicle.RegPlate,
                HaulierName = b.Haulier.Name

            });

            _bookings.Clear();
            foreach (BookingViewModel b in bookings)
            {
                _bookings.Add(b);
            }
        }
    }
}
