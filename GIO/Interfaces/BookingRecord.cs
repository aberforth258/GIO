using GIO.Interfaces;
using GIO.Models;
using GIO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class BookingRecord : IValidatableObject
    {
        public long? BookingId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CustomerReference { get; set; }
        [Required]
        public DateTime WindowStart { get; set; }
        [Required]
        public DateTime WindowEnd { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage ="Driver id must be greater than 0")]
        public long DriverId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Vehicle id must be greater than 0")]
        public long VehicleId { get; set; }
        //[Range(typeof(long?), "1", "9999999")]
        public long? TrailerId { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Haulier id must be greater than 0")]
        public long HaulierId { get; set; }
        public bool RequiresValidation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Validate window start is later than window end
            if (WindowStart >= WindowEnd)
            {
                yield return new ValidationResult("Start date cannot be later that end date.");
            }
        }
    }
}