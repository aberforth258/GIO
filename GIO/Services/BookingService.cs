using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GIO.Models;
using GIO;
using GIO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GIO.Services
{
    public class BookingService
    {
        private static GIOContext db = new GIOContext();
        //Booking calls
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns>BookingItem List required to fill Booking List on Main Page sorted by Window Start</returns>

        public static Booking CreateEditBooking(BookingRecord bookingRecord, out string[] feedback, bool bUpdate)
        {
            feedback = Array.Empty<string>();
            List<ValidationResult> errors = new List<ValidationResult>();
            if (bookingRecord.RequiresValidation)
            {

                if (TryValidateBooking(bookingRecord, out feedback))
                {
                    Booking bookingOut;

                    if (bUpdate)
                    {
                        Booking booking = db.Bookings.Single(b => b.BookingId == bookingRecord.BookingId);

                        booking.CustomerRef = bookingRecord.CustomerReference;
                        booking.BookingWindowFrom = bookingRecord.WindowStart;
                        booking.BookingWindowTo = bookingRecord.WindowEnd;
                        booking.DriverId = bookingRecord.DriverId;
                        booking.VehicleId = bookingRecord.VehicleId;
                        booking.TrailerId = bookingRecord.TrailerId;
                        booking.HaulierId = bookingRecord.HaulierId;
                        db.Bookings.Update(booking);
                        bookingOut = booking;
                    }
                    else
                    {
                         bookingOut = new Booking(bookingRecord);
                        db.Bookings.Add(bookingOut);
                    }
                    db.SaveChanges();
                    return bookingOut;
                }
            }
            feedback = errors.Select(e => e.ErrorMessage).ToArray();
            return null;
        }

        public static bool TryValidateBooking(BookingRecord bookingRecord, out string[] feedback)
        {
            bool isValid = false;
            feedback = Array.Empty<string>();
            List<ValidationResult> errors = new List<ValidationResult>();
            if (bookingRecord != null && Validator.TryValidateObject(bookingRecord, new ValidationContext(bookingRecord), errors, true))
            {
                isValid = true;
            }
            else
            {
                 feedback = errors.Select(e => e.ErrorMessage).ToArray();
            }

            return isValid;
        }
        
        #endregion

        public static bool TryDeleteBooking(long bookingId, out string feedback)
        {
            bool isSuccess = false;
            feedback = "";

            Booking booking = GetBooking(bookingId);

            if (booking != null)
            {
                try
                {
                    db.Bookings.Remove(booking);
                    db.SaveChanges();
                    isSuccess = true;
                }
                catch(Exception e)
                {
                    feedback = e.Message;
                }
            }
            else
            {
                feedback = $"Booking of id{bookingId} not found";
            }
            return isSuccess;
        }
        public static Booking GetBooking(long bookingid)
        {
            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingid);

            return booking;
        }

        public static T GetBooking<T>(Expression<Func<Booking, bool>> query, Expression<Func<Booking, T>> selector)
        {
            return db.Bookings.Where(query).Select(selector).FirstOrDefault();
        }
        public static IEnumerable<T> GetBookings<T>(Expression<Func<Booking, bool>> query, Expression<Func<Booking, T>> selector)
        {
            try
            {
                return db.Bookings.Where(query).Select(selector).ToArray();
            }
            catch(Exception e)
            {

                throw new UnauthorizedAccessException(e.Message);
            }
        }

        public static bool TryGetBooking(string regPlate, out BookingANPRRecord booking)
        {
            booking = (from b in db.Bookings
                               where b.Vehicle.RegPlate == regPlate
                               select new BookingANPRRecord(
                                   b.BookingId,
                                   b.CustomerRef,
                                    b.Driver.Name
                               )).FirstOrDefault();
            //Action<BookingANPRRecord> UpperCase = (b) => { b.DriverName = b.DriverName.ToUpper(); };
            //UpperCase(booking);

            return booking != null;
        }
    }

    public class BookingANPRRecord
    {
        public long BookingId { get; }
        public string CustomerRef { get; }
        public string DriverName { get; set; }

        public BookingANPRRecord(long bookingId, string customerRef, string driverName)
        {
            BookingId = bookingId;
            CustomerRef = customerRef;
            DriverName = driverName;
        }

        public override bool Equals(object obj)
        {
            return obj is BookingANPRRecord other &&
                   BookingId == other.BookingId &&
                   CustomerRef == other.CustomerRef &&
                   DriverName == other.DriverName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, CustomerRef, DriverName);
        }
    }
}
