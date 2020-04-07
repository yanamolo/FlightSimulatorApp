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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        FlightModel model;
        VMFlight vm;
        FlightView view;
        public Home()
        {
           InitializeComponent();
           model = new FlightModel(new Client());
           vm = new VMFlight(model);

        }

       /* private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }*/

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(IP.Text)) || (String.IsNullOrWhiteSpace(Port.Text)))
            {

            } else
            {
                vm.connect(IP.Text, Int32.Parse(Port.Text));
                vm.start();
                view = new FlightView();
                view.set_VM(vm);
                this.NavigationService.Navigate(view);
            }
        }

    }
}
