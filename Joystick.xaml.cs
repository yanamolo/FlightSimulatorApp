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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private double x;
        private double y;
        private Point elipsePoint;
        public Joystick()
        {
            InitializeComponent();
        }
        public double NormalX
        {
            get { return (double)GetValue(NormalXProperty); }
            set { SetValue(NormalXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalXProperty =
            DependencyProperty.Register("NormalX", typeof(double), typeof(Joystick), new PropertyMetadata((double)0));


        public double NormalY
        {
            get { return (double)GetValue(NormalYProperty); }
            set { SetValue(NormalYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalYProperty =
            DependencyProperty.Register("NormalY", typeof(double), typeof(Joystick), new PropertyMetadata((double)0));


        private void CenterKnob_Completed(object sender, EventArgs e)
        {
            Storyboard sb = (Storyboard)this.FindResource(this);
        }

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
                this.elipsePoint = e.GetPosition(this.border);
                if (length(x1, y1, 0, 0) < Base.Width / 3)
                {
                    knobPosition.X = x1;
                    knobPosition.Y = y1;
                    this.joystickValueTranslation();
                }
            }
        }
        private void joystickValueTranslation()
        {
            double borderRadius = this.border.Width / 2;
            double normalX = (this.knobPosition.X - this.elipsePoint.X) / borderRadius;
            double normalY = (this.knobPosition.Y - this.elipsePoint.Y) / borderRadius;
            this.NormalX = normalX;
            this.NormalY = normalY;
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