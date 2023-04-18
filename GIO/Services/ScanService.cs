using GIO.Interfaces;
using GIO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.Services
{
    public class ScanService
    {
        private static GIOContext db = new GIOContext();
        public static Scan CreateScan(ScanRecord scanRecord, out string[] feedback)
        {
            feedback = Array.Empty<string>();
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Validator.TryValidateObject(scanRecord, new ValidationContext(scanRecord), errors, true))
            {
                Scan newScan = new Scan()
                {
                    CreatedOn = DateTime.Now,
                    BookingId = scanRecord.BookingId,
                    VehicleId = scanRecord.VehicleId,
                    ScanStatusId = scanRecord.ScanStatusId,
                    ScanErrorId = scanRecord.ScanErrorId
                };

                db.Scans.Add(newScan);
                db.SaveChanges();
                return newScan;
            }
            else
            {
                feedback = errors.Select(e => e.ErrorMessage).ToArray();
                return null;
            }
            
        }
    }
}
