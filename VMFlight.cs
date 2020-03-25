using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    class VMFlight : IViewModel
    {
        private int port = 5402;
        private string ip = "127.0.0.1";
        private double ailrone;
        private double elevator;
        private double rudder;
        private double throttle;
        FlightModel model;
        public VMFlight(FlightModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropretyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropretyChanged;
        public void NotifyPropretyChanged(string propName)
        {
            if (PropretyChanged != null)
            {
                this.PropretyChanged(this, new PropertyChangedEventArgs(propName));
            }
            //PropretyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        //propreties
        public double VM_Indicated_heading_deg
        {
            get
            {
                return model.Indicated_heading_deg;
            }
        }
        public double VM_Gps_indicated_vertical_speed
        {
            get
            {
                return model.Gps_indicated_vertical_speed;
            }
        }
        public double VM_Gps_indicated_ground_speed_kt
        {
            get
            {
                return model.Gps_indicated_ground_speed_kt;
            }
        }
        public double VM_Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                return model.Airspeed_indicator_indicated_speed_kt;
            }
        }
        public double VM_Gps_indicated_altitude_ft
        {
            get
            {
                return model.Gps_indicated_altitude_ft;
            }
        }
        public double VM_Attitude_indicator_internal_roll_deg
        {
            get
            {
                return model.Attitude_indicator_internal_roll_deg;
            }
        }
        public double VM_Attitude_indicator_internal_pitch_deg
        {
            get
            {
                return model.Attitude_indicator_internal_pitch_deg;
            }
        }
        public double VM_Altimeter_indicated_altitude_ft
        {
            get
            {
                return model.Altimeter_indicated_altitude_ft;
            }
        }
        public double VM_ailron
        {
            get
            {
                return ailrone;
            }
            set
            {
                ailrone = value;
                //model.
            }
        }

        public void connect()
        {
            model.connect(ip, port);
        }
        public void start()
        {
            model.start();
        }
        public void disconnect()
        {
            model.disconnect();
        }
    }
}
