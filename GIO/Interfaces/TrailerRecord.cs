using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class TrailerRecord
    {
        [Required]
        [StringLength(10)]
        public string TrailerName { get; set; }
        [Required]
        public long TrailerTypeId { get; set; }
        [Required]
        public long HaulierId { get; set; }
        [Required]
        public bool RequiresValidation { get; set; }
    }
}
