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
using System.Windows.Threading;

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
        DispatcherTimer timer;

        public Home()
        {
            InitializeComponent();
            model = new FlightModel();
            vm = new VMFlight(model);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(3);

        }

        // The timer event handler
        void Timer_Tick(object sender, EventArgs e)
        {
            errorLable.Visibility = Visibility.Hidden;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(IP.Text)) || (String.IsNullOrWhiteSpace(Port.Text)))
            {
                // Dealing with the visibilty and time of the error lable that appears on the screen.
                this.errorLable.Content = "Missing IP or Port";
                this.errorLable.FontSize = 30;
                this.errorLable.Visibility = Visibility.Visible;
                timer.Start();

            }
            else if (!IsValidIP(IP.Text) || !IsValidPort(Port.Text))
            {
                // Dealing with the visibilty and time of the error lable that appears on the screen.
                this.errorLable.Content = "Invalid Input";
                this.errorLable.FontSize = 30;
                this.errorLable.Visibility = Visibility.Visible;
                timer.Start();
            }
            else
            {
                try
                {
                    // Trying to connect to server using the ip and port from the user.
                    model.setClient(new Client());
                    vm.connect(IP.Text, Int32.Parse(Port.Text));
                    vm.start();
                    view = new FlightView();
                    view.set_VM(vm);
                    this.NavigationService.Navigate(view);
                }
                catch (Exception)
                {
                    // Dealing with the visibilty and time of the error lable that appears on the screen.
                    this.errorLable.Content = "Server is not connected";
                    this.errorLable.Visibility = Visibility.Visible;
                    this.errorLable.FontSize = 24;
                    timer.Interval = TimeSpan.FromSeconds(5);
                    timer.Start();
                }

            }
        }

        private static bool IsValidIP(string ip)
        {
            // Spliting the array to check if there are 4 numbers in the ip string.
            string[] ipArr = ip.Split(".".ToCharArray());
            if (ipArr.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < ipArr.Length; i++)
            {
                try
                {
                    // Checking if each number is in the correct range of ip numbers.
                    int num = int.Parse(ipArr[i]);
                    if (!(num >= 0 && num <= 255))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsValidPort(string port)
        {
            try
            {
                // Checking if the given port number is leagal number between 1024 and 2^16.
                int numPort = int.Parse(port);
                if (!(numPort >= 1024 && numPort <= Math.Pow(2, 16)))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Trying to connect to server using the ip and port from App.config
                model.setClient(new Client());
                int defaultPort = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
                string defaultIP = ConfigurationManager.AppSettings["ip"].ToString();
                vm.connect(defaultIP, defaultPort);
                vm.start();
                view = new FlightView();
                view.set_VM(vm);
                this.NavigationService.Navigate(view);
            }
            catch (Exception)
            {
                // Dealing with the visibilty and time of the error lable that appears on the screen.
                this.errorLable.Visibility = Visibility.Visible;
                this.errorLable.FontSize = 24;
                this.errorLable.Content = "Server is not connected";
                timer.Interval = TimeSpan.FromSeconds(6);
                timer.Start();
            }
        }
    }
}
