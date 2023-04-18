using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.ViewModels
{
    public class TrailerTypeViewModel : ViewModelBase
    {
        public long TrailerTypeId { get; set; }
        public string TrailerTypeName { get; set; }
        public decimal TrailerTypeHeight { get; set; }
    }
}
