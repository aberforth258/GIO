using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GIO_ANPR.Utilities
{
    public static class Converters
    {
        public static Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppArgb
              );

            BitmapData data = bmp.LockBits(
              new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);

            bmp.UnlockBits(data);

            return bmp;
        }

        public static BitmapSource GetBitmapSource(Bitmap bitmapImage)
        {
            BitmapSource bitmapSource = null;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmapImage.Save(stream, ImageFormat.Bmp);
                bitmapSource = System.Windows.Media.Imaging.BitmapFrame.Create(
                                    stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

            }

            return bitmapSource;
        }
    }
}
