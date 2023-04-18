using GIO.Interfaces;
using GIO.Services;
using GIO.Utilities;
using GIO_ANPR.Utilities;
using GIO_ANPR.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GIO_ANPR.Commands
{
    public class ProcessImageCommand : CommandBase
    {
        private readonly ANPRScanViewModel _anprScanViewModel;

        public ProcessImageCommand(ANPRScanViewModel anprScanViewModel)
        {
            this._anprScanViewModel = anprScanViewModel;
        }
        public override void Execute(object parameter)
        {
            //Thread processOcr = new Thread(this.ProcessImage);
            //processOcr.Start();
            this.ProcessImage();
        }

        private async void ProcessImage()
        {
            _anprScanViewModel.ProgressText = @"Scanning picture for Reg Plate...";
            OCRService ocr = new OCRService();
            Bitmap bitmap = Converters.GetBitmap(_anprScanViewModel.ANPRImage);
            string regPlate = await ocr.ProcessImage(bitmap);
            _anprScanViewModel.ProgressText = @"Scan completed. Reg plate found: " + regPlate + " Searching database for matching booking...";
            if (!string.IsNullOrEmpty(regPlate))
            {
                ANPRBookingRecord booking = BookingService.GetBooking(b => b.Vehicle.RegPlate == regPlate, b => new ANPRBookingRecord() {
                    RegPlate = b.Vehicle.RegPlate,
                    CustomerRef = b.CustomerRef,
                    BookingId  = b.BookingId,
                    DriverName = b.Driver.Name
                });

                if(!(booking is null))
                {
                    _anprScanViewModel.ProgressText = @"Booking found...";
                    _anprScanViewModel.RegPlate = booking.RegPlate;
                    _anprScanViewModel.BookingId = booking.BookingId;
                    _anprScanViewModel.CustomerRef = booking.CustomerRef;
                    _anprScanViewModel.DriverName = booking.DriverName;

                    _anprScanViewModel.AccessGrantedVisibility = System.Windows.Visibility.Visible;
                    _anprScanViewModel.AccessDeniedVisibility = System.Windows.Visibility.Collapsed;

                    ScanRecord scan = new ScanRecord()
                    {
                        BookingId = booking.BookingId,

                    };
                }
                else
                {
                    _anprScanViewModel.ProgressText = @"Booking not found...";
                    _anprScanViewModel.AccessGrantedVisibility = System.Windows.Visibility.Collapsed;
                    _anprScanViewModel.AccessDeniedVisibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                _anprScanViewModel.ProgressText = @"Booking not found...";
                _anprScanViewModel.AccessGrantedVisibility = System.Windows.Visibility.Collapsed;
                _anprScanViewModel.AccessDeniedVisibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
