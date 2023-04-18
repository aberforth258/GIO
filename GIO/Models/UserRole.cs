using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class UserRole
    {
        public long UserRoleId { get; set; }
        public long Name { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
}
