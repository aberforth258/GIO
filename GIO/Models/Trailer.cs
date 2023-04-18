using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Trailer
    {
        public Trailer()
        {
            Bookings = new HashSet<Booking>();
            Vehicles = new HashSet<Vehicle>();
        }

        public long TrailerId { get; set; }
        public string Name { get; set; }
        public long TrailerTypeId { get; set; }
        public long HaulierId { get; set; }

        public virtual Haulier Haulier { get; set; }
        public virtual TrailerType TrailerType { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
