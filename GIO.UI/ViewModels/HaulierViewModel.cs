using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class HaulierViewModel: ViewModelBase
    {
        public long HaulierId { get; set; }
        public string HaulierName { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
