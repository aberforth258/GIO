using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Haulier
    {
        public Haulier()
        {
            Bookings = new HashSet<Booking>();
            Drivers = new HashSet<Driver>();
            Trailers = new HashSet<Trailer>();
        }

        public long HaulierId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public long CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
