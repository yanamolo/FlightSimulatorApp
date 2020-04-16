using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class VMFlight : INotifyPropertyChanged
    {
        FlightModel model;
        public VMFlight(FlightModel m)
        {
            this.model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }
        // Propreties, creating the connection between the view and the model.
        public string VM_Errors
        {
            get
            {
                return model.Errors;
            }
            set
            {
                model.Errors = value;
            }
        }
        public string VM_Errors_latitude
        {
            get
            {
                return model.Errors_latitude;
            }
            set
            {
                model.Errors_latitude = value;
            }

        }
        public string VM_Errors_longitude
        {
            get
            {
                return model.Errors_longitude;
            }
            set
            {
                model.Errors_longitude = value;
            }

        }
        public double VM_Latitude_deg
        {
            get
            {
                return model.Latitude_deg;
            }
        }
        public double VM_Longitude_deg
        {
            get
            {
                return model.Longitude_deg;
            }
        }
        public string VM_Coardinates
        {
            get
            {
                return model.Coardinates;
            }
        }

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
        public double VM_Throttle
        {
            get
            {
                return model.Throttle;
            }
            set
            {
                model.Throttle = value;
            }
        }
        public double VM_Rudder
        {
            get
            {
                return model.Rudder;
            }
            set
            {
                model.Rudder = value;
            }
        }
        public double VM_Elevator
        {
            get
            {
                return model.Elevator;
            }
            set
            {
                model.Elevator = value;
            }
        }
        public double VM_Ailrone
        {
            get
            {
                return model.Ailrone;
            }
            set
            {
                model.Ailrone = value;
            }
        }

        public void connect(string ip, int port)
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
