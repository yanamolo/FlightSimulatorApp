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

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for controlsDisplay.xaml
    /// </summary>
    public partial class controlsDisplay : UserControl
    {
        VMFlight vm;
        public controlsDisplay()
        {
            InitializeComponent();
            vm = new VMFlight(new FlightModel(new Client()));
            DataContext = vm;
/*            Binding bind_Indicated_heading_deg = new Binding("VM_Indicated_heading_deg");
            bind_Indicated_heading_deg.Mode = BindingMode.OneWay;
            bind_Indicated_heading_deg.Source = vm;
            indicated_heading.SetBinding(TextBox.TextProperty, bind_Indicated_heading_deg);
            Binding myBinding = new Binding("VM_Gps_indicated_vertical_speed");
            myBinding.Mode = BindingMode.OneWay;
            myBinding.Source = vm;
            gps_vertical.SetBinding(TextBox.TextProperty, myBinding);*/
        }
    }
}
