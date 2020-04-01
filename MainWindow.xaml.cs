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

namespace FlightSimulatorApp
{
    using FlightSimulatorApp.Controls;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VMFlight vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new VMFlight(new FlightModel(new Client()));
            this.DataContext = this.vm;
            this.controlsDisplay.DataContext = vm;
            this.Bing_Map.DataContext = vm;
            vm.connect();
            vm.start();
        }
    }
}
