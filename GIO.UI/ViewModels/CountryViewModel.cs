using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class CountryViewModel : ViewModelBase
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode2 { get; set; }
        public string CountryCode3 { get; set; }
    }
}
