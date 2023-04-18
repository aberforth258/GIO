using System;
using System.Collections.Generic;

#nullable disable

namespace GIO.Models
{
    public partial class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserManager { get; set; }
        public bool IsActive { get; set; }
        public long UserRoleId { get; set; }

        public virtual UserRole UserNavigation { get; set; }
    }
}
