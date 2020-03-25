﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulatorApp;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    class FlightModel : IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IClient telnetClient;
        volatile Boolean stop;
        //Propreties
        private double ailrone;
        private double elevator;
        private double rudder;
        private double throttle;

        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;
        public FlightModel(IClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }

        public void connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            telnetClient.Disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    telnetClient.Write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                    Indicated_heading_deg = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/gps/indicated-vertical-speed\n");
                    Gps_indicated_vertical_speed = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Gps_indicated_ground_speed_kt = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter_indicated_altitude_ft = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Attitude_indicator_internal_pitch_deg = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Attitude_indicator_internal_roll_deg = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Gps_indicated_altitude_ft = Double.Parse(telnetClient.Read());
                    telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Airspeed_indicator_indicated_speed_kt = Double.Parse(telnetClient.Read());
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }

        public double Indicated_heading_deg
        {
            get
            {
                return indicated_heading_deg;
            }
            set
            {
                indicated_heading_deg = value;
                NotifyPropretyChanged("Indicated_heading_deg");
            }
        }
        public double Gps_indicated_vertical_speed
        {
            get
            {
                return gps_indicated_vertical_speed;
            }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropretyChanged("Gps_indicated_vertical_speed");
            }
        }
        public double Gps_indicated_ground_speed_kt
        {
            get
            {
                return gps_indicated_ground_speed_kt;
            }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropretyChanged("Gps_indicated_ground_speed_kt");
            }
        }
        public double Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                return airspeed_indicator_indicated_speed_kt;
            }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropretyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }
        public double Gps_indicated_altitude_ft
        {
            get
            {
                return gps_indicated_altitude_ft;
            }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropretyChanged("Gps_indicated_altitude_ft");
            }
        }
        public double Attitude_indicator_internal_roll_deg
        {
            get
            {
                return attitude_indicator_internal_roll_deg;
            }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropretyChanged("Attitude_indicator_internal_roll_deg");
            }
        }
        public double Attitude_indicator_internal_pitch_deg
        {
            get
            {
                return attitude_indicator_internal_pitch_deg;
            }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropretyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }
        public double Altimeter_indicated_altitude_ft
        {
            get
            {
                return altimeter_indicated_altitude_ft;
            }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropretyChanged("Altimeter_indicated_altitude_ft");
            }
        }

        public double Ailrone
        {
            get
            {
                return ailrone;
            }
            set
            {
                //check if we get only -1 0 1 and not between
                ailrone = value;
                NotifyPropretyChanged("Ailrone");
            }
        }
        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                NotifyPropretyChanged("Elevator");
            }
        }
        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                if (value > 1)
                {
                    rudder = 1;
                }
                else if (value < -1)
                {
                    rudder = -1;
                }
                else
                {
                    rudder = value;
                }
                NotifyPropretyChanged("Rudder");
            }
        }
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                if (value > 1)
                {
                    throttle = 1;
                }
                else if (value < -1)
                {
                    throttle = -1;
                }
                else
                {
                    throttle = value;
                }
                NotifyPropretyChanged("Throttle");
            }
        }

        public event PropertyChangedEventHandler PropretyChanged;
        public void NotifyPropretyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            //PropretyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        /* void moveAilrone()
             {

             }
             void moveElevator()
             {

             }
             void moveRudder()
             {

             }
             void moveThrottle()
             {

             }*/
    }
}
