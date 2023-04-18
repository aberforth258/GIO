using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class TrailerType
    {
        public TrailerType()
        {
            Trailers = new HashSet<Trailer>();
        }

        public long TrailerTypeId { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }

        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
