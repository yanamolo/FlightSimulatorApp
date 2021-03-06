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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp.Controls
{
    /// <summary>
    /// Interaction logic for Bing_Map.xaml
    /// </summary>
    public partial class Bing_Map : UserControl
    {
        public Bing_Map()
        {
            InitializeComponent();
            myMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(MyMap_ViewChangeOnFrame);
        }

        public void MyMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            // Gets the map that raised this event
            Map map = (Map)sender;
            // Gets the bounded rectangle for the current frame
            LocationRect bounds = map.BoundingRectangle;
            // Update the current latitude and longitude
            CurrentPosition.Text += String.Format("Northwest: {0:F5}, Southeast: {1:F5} (Current)", bounds.Northwest, bounds.Southeast);
        }

    }
}
