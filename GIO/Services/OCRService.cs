using GIO.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage.Streams;

namespace GIO.Services
{
    public class OCRService
    {
        public async Task<string> ProcessImage(Bitmap image)
        {
            string regplate = "";

            try
            {
                //
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Bmp);

                Logger.LogInfo("Stream null:" + (stream is null));
                using (var randomAccessStream = new InMemoryRandomAccessStream())
                {
                    //BitmapImage bitImage = new BitmapImage();
                    //bitImage.BeginInit();
                    //randomAccessStream.Seek(0);


                    //Create buffer
                    image.Save(randomAccessStream.AsStream(), ImageFormat.Bmp);//choose the specific image format by your own bitmap source
                    Windows.Graphics.Imaging.BitmapDecoder decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(randomAccessStream);
                    //Logger.LogInfo("Random access stream null:" + (randomAccessStream is null));

                    Logger.LogInfo("Decoder null:" + (decoder is null));
                    using (var softwareBitmap = await decoder.GetSoftwareBitmapAsync())
                    {
                        Logger.LogInfo("sBitmap null:" + (softwareBitmap is null));

                        regplate = await GetTextFromImage(softwareBitmap);

                        if (BookingService.TryGetBooking(regplate, out BookingANPRRecord booking))
                        {
                            //Grant access
                        }
                        else
                        {
                            //restrict access
                        }

                        stream.Dispose();
                        decoder = null;
                        GC.Collect();



                    }

                }


            }
            catch (Exception ex)
            {
                //throw exception
            }
            finally
            {
                //Clean form
            }
            return regplate;
        }

        private async Task<string> GetTextFromImage(SoftwareBitmap image)
        {

            Language lang = new Language("en");
            OcrEngine ocr = OcrEngine.TryCreateFromLanguage(lang);
            OcrResult result = null;
            //Convert Bitmap to SoftwareBitmap

            try
            {
                result = await ocr.RecognizeAsync(image);
                return result is null ? "" : result.Text;
            }
            catch (Exception e)
            {
                return "Error";
            }


        }

        public async Task<string> GetTextFromImage(Bitmap image)
        {

            Language lang = new Language("en");
            Bitmap newBitmap = new Bitmap(image);
            OcrEngine ocr = OcrEngine.TryCreateFromLanguage(lang);
            OcrResult result = null;
            //Convert Bitmap to SoftwareBitmap

            using (Windows.Storage.Streams.InMemoryRandomAccessStream stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                if (newBitmap != null)
                {

                    newBitmap.Save(stream.AsStream(), ImageFormat.Tiff); //choose the specific image format by your own bitmap source
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                    using (SoftwareBitmap s_Bitmap = await decoder.GetSoftwareBitmapAsync())
                    {
                        try
                        {
                            result = await ocr.RecognizeAsync(s_Bitmap);
                            newBitmap.Dispose();
                            image.Dispose();

                            return result is null ? "" : result.Text;
                        }
                        catch (Exception e)
                        {
                            return "Error";
                        }

                    }
                }
                else
                {
                    return "Image is null";
                }
            }
        }
    }
}
