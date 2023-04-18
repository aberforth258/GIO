using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class TrailerViewModel : ViewModelBase
    {
        public long TrailerId { get; set; }
        public string TrailerName { get; set; }
        public string TrailerType { get; set; }
        public string HaulierName { get; set; }

    }
}
