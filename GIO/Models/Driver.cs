using System;
using System.Collections.Generic;
using GIO.Interfaces;

#nullable disable

namespace GIO.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Bookings = new HashSet<Booking>();
        }

        /// <summary>
        /// Instantiates the object using interface object DriverRecord
        /// </summary>
        /// <param name="driverRecord">DrivingRecord object Driver should be based on</param>
        public Driver(DriverRecord driverRecord)
        {
            Bookings = new HashSet<Booking>();

            Name = driverRecord.DriverName;
            Phone = driverRecord.Phone;
            ManagerName = driverRecord.ManagerName;
            HaulierId = driverRecord.HaulierId;
        }

        public long DriverId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ManagerName { get; set; }
        public long HaulierId { get; set; }

        public virtual Haulier Haulier { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
