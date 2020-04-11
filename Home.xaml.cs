using System;
using System.Collections.Generic;
using System.Configuration;
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
           model = new FlightModel();
           vm = new VMFlight(model);

        }

       /* private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }*/

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(IP.Text)) || (String.IsNullOrWhiteSpace(Port.Text)))
            {
                this.errorLable.Content = "Missing IP or Port";
            }
            else if (!isValidIP(IP.Text) || !isValidPort(Port.Text))
            {
                this.errorLable.Content = "Invalid Input";
            }
            else
            {
                try
                {
                    model.setClient(new Client());
                    vm.connect(IP.Text, Int32.Parse(Port.Text));
                    vm.start();
                    view = new FlightView();
                    view.set_VM(vm);
                    this.NavigationService.Navigate(view);
                }
                catch (Exception exception)
                {
                    this.errorLable.Content = "Server is not connected";
                }

            }
        }

        private static bool isValidIP(string ip)
        {
            string[] ipArr = ip.Split(".".ToCharArray());
            if (ipArr.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < ipArr.Length; i++)
            {
                try
                {
                    int num = int.Parse(ipArr[i]);
                    if (!(num >= 0 && num <= 255))
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool isValidPort(string port)
        {
            try
            {
                int numPort = int.Parse(port);
                if (!(numPort >= 1024 && numPort <= Math.Pow(2, 16)))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private void default_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                model.setClient(new Client());
                int defaultPort = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
                string defaultIP = ConfigurationManager.AppSettings["ip"].ToString();
                vm.connect(defaultIP, defaultPort);
                vm.start();
                view = new FlightView();
                view.set_VM(vm);
                this.NavigationService.Navigate(view);
            }
            catch (Exception ex)
            {
                this.errorLable.Content = "Server is not connected";
            }
        }
    }
}
