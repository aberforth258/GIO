using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.Interfaces
{
    public class ANPRBookingRecord
    {
        public long? BookingId { get; set; }
        public string RegPlate { get; set; }
        public string CustomerRef { get; set; }
        public string DriverName { get; set; }

    }
}
