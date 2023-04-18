using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class VehicleViewModel :ViewModelBase
    {
        public long VehicleId { get; set; }
        public string VehicleRegPlate { get; set; }
        public bool IsBanned { get; set; }
}
}
