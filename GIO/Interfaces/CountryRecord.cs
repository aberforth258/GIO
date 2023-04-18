using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class CountryRecord
    {
        [Required]
        public string CountryName  {get; set;}
        [Required]
        [StringLength(2)]
        public string CountryCode2 {get; set;}
        [Required]
        [StringLength(3)]
        public string CountryCode3 { get; set; }
    }
}
