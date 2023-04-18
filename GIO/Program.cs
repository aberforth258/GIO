using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GIO.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Globalization;

namespace GIO
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            string filePath = @"C:\temp\J98257.jpg";

            Bitmap bitmap = new Bitmap(filePath);
            
            Language lang = new Language("en");

            OcrEngine ocr = OcrEngine.TryCreateFromLanguage(lang);

            //Convert Bitmap to SoftwareBitmap
            SoftwareBitmap s_Bitmap;
            using (Windows.Storage.Streams.InMemoryRandomAccessStream stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                bitmap.Save(stream.AsStream(), bitmap.RawFormat); //choose the specific image format by your own bitmap source
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                s_Bitmap = await decoder.GetSoftwareBitmapAsync();
            }

            OcrResult result = await ocr.RecognizeAsync(s_Bitmap);

            Console.WriteLine(result.Text);
            

            return;
        }
    }
}