using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class TrailerTypeRecord
    {
        [Required]
        [MaxLength(100)]
        public string TrailerTypeName { get; set; }
        [Required]
        public decimal Height { get; set; }
    }
}
