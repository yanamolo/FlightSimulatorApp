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
    /// Interaction logic for FlightView.xaml
    /// </summary>
    public partial class FlightView : Page
    {
        VMFlight vm;
        public FlightView()
        {
            InitializeComponent();
        }

        public void set_VM(VMFlight viewModel)
        {
            vm = viewModel;
            DataContext = vm;
            this.controlsDisplay.DataContext = vm;
            this.Bing_Map.DataContext = vm;
            this.JoystickSliders.DataContext = vm;
        }
        private void locationButton_Click(object sender, RoutedEventArgs e)
        {
            if (location_of_pushpin.Visibility == Visibility.Visible)
            {
                location_of_pushpin.Visibility = Visibility.Hidden;
                locationButton.Content =  "   SHOW\nLOCATION";

            } else if (location_of_pushpin.Visibility == Visibility.Hidden)
            {
                location_of_pushpin.Visibility = Visibility.Visible;
                locationButton.Content = "    HIDE\nLOCATION";
            }
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
         {
            vm.disconnect();
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No entries in back navigation history.");
            }
        }
    }
}
