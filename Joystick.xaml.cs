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
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private double x;
        private double y;
        public Joystick()
        {
            InitializeComponent();
        }
        private void CenterKnob_Completed(object sender, EventArgs e) { }

        private double length(double x, double y, double x1, double y1)
        {
            return Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y));
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x1;
            double y1;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x1 = e.GetPosition(this).X - x;
                y1 = e.GetPosition(this).Y - y;
                if (length(x1, y1, 0, 0) < Base.Width / 4)
                {
                    knobPosition.X = x1;
                    knobPosition.Y = y1;
                }
            }
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                x = e.GetPosition(this).X;
                y = e.GetPosition(this).Y;
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}