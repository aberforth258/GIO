using GIO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;
using System.Drawing;

namespace GIO.Services
{
    public class CameraService
    {
        private VideoCapture capture;
        private Mat frame = new Mat();
        private Thread camera;
        private bool isCameraRunning = false;
        public CameraService()
        {
            capture = new VideoCapture();
            capture.FrameWidth = 2160;
            capture.FrameHeight = 3840;
            capture.AutoFocus = true;
            capture.AutoExposure = 0.2;
        }

        public void Open()
        {
            capture.Open(0);
        }

        public void Close()
        {
            capture.Dispose();
        }
        public Bitmap GetCameraSnapShot()
        {
            frame = new Mat();

            if (!capture.IsOpened()) this.Open();
            capture.Read(frame);
                
            Bitmap bitMapOut = null;
            try
            {
                bitMapOut = BitmapConverter.ToBitmap(frame);
            }
            catch(Exception e)
            {

            }

            return bitMapOut;
        }

    }
}
