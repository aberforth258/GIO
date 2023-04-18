using GIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    class DriverViewModel : ViewModelBase
    {
        public long DriverId { get; set; }
        public string DriverName { get; set; }
        public string HaulierName { get; set; }

        public DriverViewModel()
        {
        }

    }
}
