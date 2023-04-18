using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class ScanError
    {
        public ScanError()
        {
            Scans = new HashSet<Scan>();
        }

        public long ScanErrorId { get; set; }
        public string ErrorDescription { get; set; }

        public virtual ICollection<Scan> Scans { get; set; }
    }
}
