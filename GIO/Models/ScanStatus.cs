using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class ScanStatus
    {
        public ScanStatus()
        {
            Scans = new HashSet<Scan>();
        }

        public long ScanStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Scan> Scans { get; set; }
    }
}
