using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GIO.Interfaces
{
    public class HaulierRecord : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string HaulierName { get; set; }
        [Required]
        [StringLength(100)]
        public string HaulierAddressLine1 { get; set; }
        [StringLength(100)]
        public string HaulierAddressLine2 { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression( @"/^[A-Za-z]{1,2}\d{1,2}\d{1,2}[A-Za-z]{2,3}$/")] //Found online, apparently regex provided by GOV?
        public string PostCode { get; set; }

        [Required]
        public long? CountryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Validate Range[1,99999] as attribute does not work
            if(CountryId.HasValue && CountryId.Value < 1 )
            {
                yield return new ValidationResult("Country must be provided.");
            }
        }
        
    }
}

