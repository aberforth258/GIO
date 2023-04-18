using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Bookings = new HashSet<Booking>();
            Scans = new HashSet<Scan>();
        }

        public long VehicleId { get; set; }
        public string RegPlate { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? LastSeen { get; set; }
        public long? LastTrailerId { get; set; }
        public long VehicleTypeId { get; set; }

        public virtual Trailer LastTrailer { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Scan> Scans { get; set; }
    }
}
