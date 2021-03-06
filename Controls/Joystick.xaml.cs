﻿using System;
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
        private double toX, toY;
        private Point knobCenter;
        private Point elipsePoint;
        private volatile bool mousePressed;
        public Joystick()
        {
            InitializeComponent();
            // Creating the initialized x and y of the joystick.
            knobCenter = new Point(this.Base.Width / 2, this.Base.Height / 2);
            toX = knobCenter.X;
            toY = knobCenter.Y;
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


        private double length(double x, double y, double x1, double y1)
        {
            return Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y));
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mousePressed)
            {
                // Placing the new x and y that we got from the mouse move.
                toX = e.GetPosition(this.Base).X - knobCenter.X;
                toY = e.GetPosition(this.Base).Y - knobCenter.Y;
                this.elipsePoint = e.GetPosition(this.border);
                if (inBound())
                {
                    moveKnob();
                }
                else
                {
                    moveKnobToCenter();
                }
            }
        }

        private bool inBound()
        {
            // Checking if the new x and y points of the knob are in bound using an ellipse formula.
            double bound = Math.Pow(toX / (this.border.Width / 2), 2) + Math.Pow(toY / (this.border.Height / 2), 2);
            return bound <= 1;
        }

        private void joystickValueTranslation()
        {
            // Normalizing the x and y points by subtructing the ellipse center from the elipse point
            // which is the border ellipse, and dividing the result with the border radiuse.
            double borderRadius = this.border.Width / 2;
            Point ellipseCenter = new Point(this.border.Width / 2, this.border.Height / 2);
            double normalX = (this.elipsePoint.X - ellipseCenter.X) / borderRadius;
            double normalY = (this.elipsePoint.Y - ellipseCenter.Y) / borderRadius;
            this.NormalX = normalX;
            this.NormalY = -normalY;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.mousePressed = true;
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.mousePressed)
            {
                this.moveKnobToCenter();
            }
        }

        private void moveKnob()
        {
            // Activating the animation of the knob.
            Storyboard sb = this.Knob.Resources["MoveKnob"] as Storyboard;
            DoubleAnimation animX = sb.Children[0] as DoubleAnimation;
            DoubleAnimation animY = sb.Children[1] as DoubleAnimation;
            animX.To = toX;
            animY.To = toY;
            sb.Begin();
            animX.From = toX;
            animY.From = toY;
            this.joystickValueTranslation();
        }

        private void moveKnobToCenter()
        {
            this.mousePressed = false;
            toX = 0;
            toY = 0;
            this.moveKnob();
        }
    }
}