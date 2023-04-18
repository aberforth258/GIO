using GIO.Interfaces;
using GIO.Services;
using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Scans = new HashSet<Scan>();
        }

        public Booking(BookingRecord bookingRecord)
        {
            CreatedOn = DateTime.Now;
            CustomerRef = bookingRecord.CustomerReference;
            BookingWindowFrom = bookingRecord.WindowStart;
            BookingWindowTo = bookingRecord.WindowEnd;
            DriverId = bookingRecord.DriverId;
            VehicleId = bookingRecord.VehicleId;
            TrailerId = bookingRecord.TrailerId;
            HaulierId = bookingRecord.HaulierId;
            BookingStatusId = (long)StatusService.Statuses.Created;
        }

        

        public long BookingId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CustomerRef { get; set; }
        public DateTime BookingWindowFrom { get; set; }
        public DateTime BookingWindowTo { get; set; }
        public long DriverId { get; set; }
        public long VehicleId { get; set; }
        public long? TrailerId { get; set; }
        public long HaulierId { get; set; }
        public long BookingStatusId { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Haulier Haulier { get; set; }
        public virtual Trailer Trailer { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Scan> Scans { get; set; }
    }
}
