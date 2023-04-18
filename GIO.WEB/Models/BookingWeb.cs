using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GIO.WEB.Models
{
    public class BookingWeb
    {
        public long BookingId { get; set; }
        public string CustomerReference { get; set; }
        public string Status { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime WindowStart { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        
        public DateTime WindowEnd { get; set; }
        public long DriverId { get; set; }
        public string DriverName { get; set; }
        public long VehicleId { get; set; }
        public string VehicleRegPlate { get; set; }
        public long? TrailerId { get; set; }
        public string TrailerName { get; set; }
        public long HaulierId { get; set; }
        public string HaulierName { get; set; }

        public BookingWeb()
        {
            WindowStart = DateTime.Now.Subtract(TimeSpan.FromHours(6));
            WindowEnd = DateTime.Now.AddHours(6);
        }
    }
}
