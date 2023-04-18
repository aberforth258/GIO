using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class VehicleTypeRecord
    {
        [Required]
        [MaxLength(50)]
        public string VehicleTypeName { get; set; }
        public bool RequiresValidation { get; set; }
    }
}
