using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.Services
{
    public class StatusService
    {
        public enum Statuses{
            NotUsed,
            Created,
            Arrived,
            Cancelled,
            Rejected,
            Completed

        }
    }
}
