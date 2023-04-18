using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIO.UI.Views
{
    /// <summary>
    /// Interaction logic for VehicleSelectionList.xaml
    /// </summary>
    public partial class VehicleSelectionList : UserControl
    {
        public VehicleSelectionList()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectVehicleButton.Command.Execute(this);
        }
    }
}
