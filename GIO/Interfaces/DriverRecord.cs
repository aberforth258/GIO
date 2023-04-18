using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GIO.Interfaces
{
    public class DriverRecord
    {
        [Required]
        [StringLength(100)]
        public string DriverName { get; set; }

        [Required]
        [StringLength(20)]
        //[RegularExpression("(07[0 - 9]{3} ?[0 - 9]{6})", ErrorMessage = "Incorrect Format for phone. (07XXX XXXXXX)")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string ManagerName { get; set; }

        [Required]
        public long HaulierId { get; set; }

        [Required]
        public bool RequiresValidation { get; set; }

    }
}
