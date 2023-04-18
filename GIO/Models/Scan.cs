using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Scan
    {
        public long ScanId { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? VehicleId { get; set; }
        public long? BookingId { get; set; }
        public long ScanStatusId { get; set; }
        public long? ScanErrorId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual ScanError ScanError { get; set; }
        public virtual ScanStatus ScanStatus { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
