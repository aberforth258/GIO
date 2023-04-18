using GIO.Services;
using GIO_ANPR.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GIO_ANPR.Commands
{
    public class FetchCameraSnapshotCommand : CommandBase
    {
        private readonly ANPRScanViewModel _anprView;
        private CameraService camera = new CameraService();

        public FetchCameraSnapshotCommand(ANPRScanViewModel anprView)
        {
            this._anprView = anprView;
        }

        public override void Execute(object parameter)
        {
            Thread camera = new Thread(FetchCameraSnapshot);
            camera.Start();
        }
        private void FetchCameraSnapshot()
        {
            DateTime timeStart = DateTime.Now;

            _anprView.ProgressText = @"Taking picture... Hold still and smile :)";
            _anprView.ProgressTextVisibility = Visibility.Visible;
            Bitmap bitmapImage = camera.GetCameraSnapShot();

            if (bitmapImage != null)
            {
                _anprView.ANPRImage = Utilities.Converters.GetBitmapSource(bitmapImage);
                _anprView.ProgressText = @"Picture taken in " + (DateTime.Now.Subtract(timeStart)).Seconds + "s";
                //_anprView.AccessGrantedVisibility = Visibility.Visible;
                //_anprView.AccessDeniedVisibility = Visibility.Collapsed;

            }
            else
            {
                //_anprView.AccessGrantedVisibility = Visibility.Collapsed;
                //_anprView.AccessDeniedVisibility = Visibility.Visible;
            }
        }
    }
}
