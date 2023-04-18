using GIO.Interfaces;
using GIO.Services;
using GIO.Utilities;
using GIO_ANPR.Commands;
using GIO_ANPR.Stores;
using GIO_ANPR.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WhiteStarPlanner.Commands;

namespace GIO_ANPR.ViewModels
{
    public class ANPRScanViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private BitmapSource _anprImage;
        public BitmapSource ANPRImage
        {
            get
            {
                return _anprImage;
            }
            set
            {
                _anprImage = value;
                OnPropertyChanged(nameof(ANPRImage));
            }
        }

        private string progressText;
        public string ProgressText
        {
            get
            {
                return progressText;
            }
            set
            {
                progressText = value;
                OnPropertyChanged(nameof(ProgressText));
            }
        }

        private string _regPlate;
        public string RegPlate
        {
            get
            {
                return _regPlate;
            }
            set
            {
                _regPlate = value;
                OnPropertyChanged(nameof(RegPlate));
            }
        }
        private long? _bookingId;
        public long? BookingId
        {
            get
            {
                return _bookingId;
            }
            set
            {
                _bookingId = value;
                OnPropertyChanged(nameof(BookingId));
            }
        }
        private string _customerRef;
        public string CustomerRef
        {
            get
            {
                return _customerRef;
            }
            set
            {
                _customerRef = value;
                OnPropertyChanged(nameof(CustomerRef));
            }
        }

        private string _driverName;
        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(DriverName));
            }
        }

        private Visibility _accessGratedVisibility;
        public Visibility AccessGrantedVisibility
        {
            get
            {
                return _accessGratedVisibility;
            }
            set
            {
                _accessGratedVisibility = value;
                OnPropertyChanged(nameof(AccessGrantedVisibility));
            }
        }
        private Visibility _accessDeniedVisibility;
        public Visibility AccessDeniedVisibility
        {
            get
            {
                return _accessDeniedVisibility;
            }
            set
            {
                _accessDeniedVisibility = value;
                OnPropertyChanged(nameof(AccessDeniedVisibility));
            }
        }

        private Visibility progressTextVisibility;
        public Visibility ProgressTextVisibility
        {
            get
            {
                return progressTextVisibility;
            }
            set
            {
                progressTextVisibility = value;
                OnPropertyChanged(nameof(ProgressTextVisibility));
            }
        }

        public ICommand FetchCameraSnapshotCommand { get; }
        public ICommand ProcessImageCommand { get; }
        public ICommand StartCameraCommand { get; }

        private Timer CameraSnapshotOnTimerTimer;
        private CameraService camera = new CameraService();
        private bool isCameraTimerRunning = false;
        private bool isProcessingImage = false;

        public ANPRScanViewModel(NavigationStore navigationStore)
        {

            this._navigationStore = navigationStore;

            RegPlate = string.Empty;
            BookingId = null;
            CustomerRef = string.Empty;
            DriverName = string.Empty;
            AccessGrantedVisibility = Visibility.Collapsed;
            AccessDeniedVisibility = Visibility.Collapsed;

            //FetchCameraSnapshotCommand = new FetchCameraSnapshotCommand(this);
            FetchCameraSnapshotCommand = new RelayCommand<object>(o => CameraShapshotTimer(10000));
            ProcessImageCommand = new ProcessImageCommand(this);
        }

        private void FetchCameraSnapshot(Object stateInfo)
        {
            if (!isProcessingImage) //Make sure the image processor is not running
            {
                DateTime timeStart = DateTime.Now;

                this.ProgressText = @"Taking picture... Hold still and smile :)";
                this.ProgressTextVisibility = Visibility.Visible;
                Bitmap bitmapImage = camera.GetCameraSnapShot();

                if (bitmapImage != null)
                {
                    this.ANPRImage = Utilities.Converters.GetBitmapSource(bitmapImage);
                    this.ProgressText = @"Picture taken in " + (DateTime.Now.Subtract(timeStart)).Seconds + "s";

                    ProcessImageAsync();
                }
                else
                {
                    this.ProgressText = @"Failed to load the picture...";
                }
            }
        }

        /// <summary>
        /// Fetches camera snapshot at specified interval
        /// </summary>
        /// <param name="interval">Time interval in milliseconds</param>
        /// <param name="action">Action to perform: 0 - Stop; 1 - Start</param>
        private void CameraShapshotTimer(int interval)
        {
            if(!isCameraTimerRunning)
            {
                try
                {
                    CameraSnapshotOnTimerTimer = new Timer(FetchCameraSnapshot, null, 0,interval);
                    isCameraTimerRunning = true;
                }
                catch(Exception e)
                {
                    Logger.LogError(e.Message);
                }
            }
            else
            {
                try
                {
                    CameraSnapshotOnTimerTimer.Dispose();
                    isCameraTimerRunning = false;
                    this.ProgressText = "Camera stopped...";
                }
                catch(Exception e)
                {
                    Logger.LogError(e.Message);
                }
            }

        }

        private async void ProcessImageAsync()
        {
            isProcessingImage = true;
            this.ProgressText = @"Scanning picture for Reg Plate...";
            OCRService ocr = new OCRService();
            //Array pixels = Array.Empty();
            //this.ANPRImage.CopyPixels(pixels, 0, 0);

            try
            {
                BitmapSource bitmapsourceCopy = this.ANPRImage.Clone(); //Clone picture so changes from original are not affected.
                Bitmap bitmap = Converters.GetBitmap(bitmapsourceCopy);
                string regPlate = await ocr.ProcessImage(bitmap);
                this.ProgressText = @"Scan completed. Reg plate found: " + regPlate + " Searching database for matching booking...";
                if (!string.IsNullOrEmpty(regPlate))
                {
                    ANPRBookingRecord booking = BookingService.GetBooking(b => b.Vehicle.RegPlate == regPlate, b => new ANPRBookingRecord()
                    {
                        RegPlate = b.Vehicle.RegPlate,
                        CustomerRef = b.CustomerRef,
                        BookingId = b.BookingId,
                        DriverName = b.Driver.Name
                    });

                    if (!(booking is null))
                    {
                        this.ProgressText = @"Booking found...";
                        this.RegPlate = booking.RegPlate;
                        this.BookingId = booking.BookingId;
                        this.CustomerRef = booking.CustomerRef;
                        this.DriverName = booking.DriverName;

                        this.AccessGrantedVisibility = System.Windows.Visibility.Visible;
                        this.AccessDeniedVisibility = System.Windows.Visibility.Collapsed;

                        CameraShapshotTimer(3000);
                        ScanRecord scan = new ScanRecord()
                        {
                            BookingId = booking.BookingId,

                        };
                    }
                    else
                    {
                        this.ProgressText = @"Booking not found...";
                        this.RegPlate = regPlate;
                        this.AccessGrantedVisibility = System.Windows.Visibility.Collapsed;
                        this.AccessDeniedVisibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    this.ProgressText = @"Booking not found...";
                    this.AccessGrantedVisibility = System.Windows.Visibility.Collapsed;
                    this.AccessDeniedVisibility = System.Windows.Visibility.Visible;
                }

                isProcessingImage = false;
            }
            catch(Exception ex)
            {
                this.ProgressText = ex.Message;
            }
            //BitmapSource b = BitmapSource.Create(this.ANPRImage.PixelWidth,
            //                                                this.ANPRImage.PixelHeight,
            //                                                this.ANPRImage.DpiX,
            //                                                this.ANPRImage.DpiY,
            //                                                this.ANPRImage.Format,
            //                                                this.ANPRImage.Palette,
            //                                                pixels,
            //                                                0);

            
        }
    }
}
