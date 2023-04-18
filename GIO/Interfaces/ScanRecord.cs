using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.Interfaces
{
    public class ScanRecord
    {
        [Required]
        public DateTime CreatedOn { get; set; }
        public long? VehicleId { get; set; }
        public long? BookingId { get; set; }
        [Required]
        public long ScanStatusId { get; set; }
        public long? ScanErrorId { get; set; }

    }
}
