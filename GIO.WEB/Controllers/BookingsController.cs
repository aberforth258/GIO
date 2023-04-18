using Microsoft.AspNetCore.Mvc;
using GIO.Services;
using System.Collections;
using System.Collections.Generic;
using GIO.WEB.Models;
using GIO.Utilities;
using GIO.Interfaces;

namespace GIO.WEB.Controllers
{
    public class BookingsController : Controller
    {

        private IEnumerable<BookingWeb> GetBookings()
        {
            return BookingService.GetBookings<BookingWeb>(b => true, b => new BookingWeb()
            {
                BookingId = b.BookingId,
                CustomerReference = b.CustomerRef,
                WindowStart = b.BookingWindowFrom,
                WindowEnd = b.BookingWindowTo,
                DriverName = b.Driver.Name,
                VehicleRegPlate = b.Vehicle.RegPlate,
                TrailerName = b.Trailer.Name
            });
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetList()
        {


            return View(GetBookings());
        }

        public IActionResult CreateBooking()
        {
            return View(new BookingWeb());
        }

        public IActionResult EditBooking(long bookingId)
        {
            BookingWeb booking = BookingService.GetBooking<BookingWeb>(b => b.BookingId == bookingId, b => new BookingWeb()
            {
                BookingId = b.BookingId,
                CustomerReference = b.CustomerRef,
                WindowStart = b.BookingWindowFrom,
                WindowEnd = b.BookingWindowTo,
                DriverId = b.DriverId,
                VehicleId = b.VehicleId,
                TrailerId = b.TrailerId,
                HaulierId = b.HaulierId
            });
            return View(booking);
        }

        [HttpPost]
        public IActionResult SubmitCreateBooking(BookingWeb booking)
        {
            BookingRecord bookingRecord = new BookingRecord()
            {
                CustomerReference = booking.CustomerReference,
                WindowStart = booking.WindowStart,
                WindowEnd = booking.WindowEnd,
                DriverId = booking.DriverId,
                VehicleId = booking.VehicleId,
                TrailerId = booking.TrailerId,
                HaulierId = booking.HaulierId,
                RequiresValidation = true
            };
            string[] feedback;
            BookingService.CreateEditBooking(bookingRecord, out feedback, false);
            
            return View("GetList", GetBookings());
        }

        [HttpPost]
        public IActionResult SubmitEditBooking(BookingWeb booking)
        {
            BookingRecord bookingRecord = new BookingRecord()
            {
                BookingId = booking.BookingId,
                CustomerReference = booking.CustomerReference,
                WindowStart = booking.WindowStart,
                WindowEnd = booking.WindowEnd,
                DriverId = booking.DriverId,
                VehicleId = booking.VehicleId,
                TrailerId = booking.TrailerId,
                HaulierId = booking.HaulierId,
                RequiresValidation = true
            };
            string[] feedback;
            BookingService.CreateEditBooking(bookingRecord, out feedback, true);
            return View("GetList", GetBookings());
        }

        public IActionResult SubmitDeleteBooking(long bookingId)
        {
            string feedback;
            BookingService.TryDeleteBooking(bookingId, out feedback);
            return View("GetList", GetBookings());
        }
            
    }
}
