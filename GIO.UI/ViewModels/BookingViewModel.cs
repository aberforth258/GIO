using GIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class BookingViewModel : ViewModelBase
    {
        public long BookingId { get; set; }
        public string CustomerReference { get; set; }
        public string Status { get; set; }
        public DateTime WindowStart { get; set; }
        public DateTime WindowEnd { get; set; }
        public string DriverName { get; set; }
        public string VehicleRegPlate { get; set; }
        public string TrailerName { get; set; }
        public string HaulierName { get; set; }

        public override bool Equals(object obj)
        {
            BookingViewModel b = obj as BookingViewModel;
            if (b != null)
                return BookingId == b.BookingId;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerReference, Status, WindowStart, WindowEnd, DriverName, VehicleRegPlate, TrailerName, HaulierName);
        }
    }
}
