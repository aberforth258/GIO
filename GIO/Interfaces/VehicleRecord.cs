using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class VehicleRecord
    {
        [Required]
        [StringLength(10)]
        public string RegPlate { get; set; }
        [Required]
        public bool IsBanned { get; set; }
        [Required]
        public long VehicleTypeId { get; set; }
    }
}
