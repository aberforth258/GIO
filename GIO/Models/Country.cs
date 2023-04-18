using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class Country
    {
        public Country()
        {
            Hauliers = new HashSet<Haulier>();
        }

        public long CountryId { get; set; }
        public string Name { get; set; }
        public string CountryCode2 { get; set; }
        public string CountryCode3 { get; set; }

        public virtual ICollection<Haulier> Hauliers { get; set; }
    }
}
