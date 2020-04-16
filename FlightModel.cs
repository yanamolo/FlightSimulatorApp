using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp
{
    public class FlightModel : IModel
    {
        private readonly object balancelock = new object();
        List<string> msg_to_send;
        IClient telnetClient;
        volatile bool stop;

        //Propreties.
        private string errors;
        private string errors_latitude;
        private string errors_longitude;

        private double latitude_deg;
        private double longitude_deg;
        private string latitude;
        private string longitude;
        private string coardinates;

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

        public FlightModel()
        {
            stop = false;
            msg_to_send = new List<string>();
        }

        public void setClient(IClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }

        public void connect(string ip, int port)
        {
            stop = false;
            telnetClient.Connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            telnetClient.Disconnect();
        }

        // While server is open and connected read data as follows-
        // Write a massage with the right order of getting a certain data.
        // Read into that data property the information. Transform to Double type if its needed.
        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
                        // Locking before using the list. The main thread is also using this list.
                        // First we send the "set" requests, and then proceed to the "get" request to
                        // avoid collision.
                        lock (balancelock)
                        {
                            if (this.msg_to_send.Count != 0)
                            {
                                int i = 0;
                                do
                                {
                                    this.telnetClient.Write(msg_to_send[i]);
                                    string temp = this.telnetClient.Read();
                                    msg_to_send.Remove(msg_to_send[i]);
                                } while (this.msg_to_send.Count != 0);
                            }
                        }

                        // Save a string field and a double property of latitude and logitude.
                        // In the end save a string of full Coardinates property.
                        telnetClient.Write("get /position/latitude-deg\r\n");
                        latitude = telnetClient.Read();
                        Latitude_deg = Double.Parse(latitude);
                        telnetClient.Write("get /position/longitude-deg\r\n");
                        longitude = telnetClient.Read();
                        Longitude_deg = Double.Parse(longitude);
                        Coardinates = latitude + "," + longitude;

                        // All the getters for the control pannel
                        telnetClient.Write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                        Indicated_heading_deg = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                        Gps_indicated_vertical_speed = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                        Gps_indicated_ground_speed_kt = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                        Altimeter_indicated_altitude_ft = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                        Attitude_indicator_internal_pitch_deg = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                        Attitude_indicator_internal_roll_deg = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                        Gps_indicated_altitude_ft = Double.Parse(telnetClient.Read());
                        telnetClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                        Airspeed_indicator_indicated_speed_kt = Double.Parse(telnetClient.Read());
                        // Read the data in 4Hz
                        Thread.Sleep(250);
                    }
                    catch (TimeoutException e)
                    {
                        Errors = e.Message;
                        continue;
                    }
                    catch
                    {
                        if (!this.telnetClient.isConnected())
                        {
                            this.disconnect();
                            Errors = "Server Disconnected, please log out";
                        }
                    }
                }
            }).Start();
        }
        // All the properties for our program.
        public string Errors
        {
            get
            {
                return this.errors;
            }
            set
            {
                this.errors = value;
                NotifyPropertyChanged("Errors");
            }
        }
        public string Errors_latitude
        {
            get
            {
                return this.errors_latitude;
            }
            set
            {
                this.errors_latitude = value;
                NotifyPropertyChanged("Errors_latitude");
            }
        }
        public string Errors_longitude
        {
            get
            {
                return this.errors_longitude;
            }
            set
            {
                this.errors_longitude = value;
                NotifyPropertyChanged("Errors_longitude");
            }
        }
        public double Latitude_deg
        {
            get
            {
                return latitude_deg;
            }
            set
            {
                if ((value <= 90) && (value >= -90))
                {
                    latitude_deg = value;
                    latitude = latitude.Substring(0, latitude.Length - 1);
                }
                else if (latitude_deg == 0)
                {
                    if (value > 90)
                    {
                        latitude_deg = 90; ;
                    }
                    else
                    {
                        latitude_deg = -90;
                    }
                    latitude = latitude.Substring(0, latitude.Length - 1);
                    Errors_latitude = "Latitude is out of Range";

                }
                else
                {
                    latitude = latitude_deg.ToString();
                    Errors_latitude = "Latitude is out of Range";
                }
                NotifyPropertyChanged("Latitude_deg");
            }
        }
        public double Longitude_deg
        {
            get
            {
                return longitude_deg;
            }
            set
            {
                if ((value <= 180) && (value >= -180))
                {
                    longitude_deg = value;
                    longitude = longitude.Substring(0, longitude.Length - 1);
                }
                else if (longitude_deg == 0)
                {

                    if (value > 180)
                    {
                        longitude_deg = 180;
                    }
                    else
                    {
                        longitude_deg = -180;
                    }
                    longitude = longitude.Substring(0, longitude.Length - 1);
                    Errors_longitude = "Longitude is out of Range";
                }
                else
                {
                    longitude = longitude_deg.ToString();
                    Errors_longitude = "Longitude is out of Range";
                }
                NotifyPropertyChanged("Longitude_deg");
            }
        }
        public string Coardinates
        {
            get
            {
                return coardinates;
            }
            set
            {
                coardinates = value;
                NotifyPropertyChanged("Coardinates");
            }
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
                NotifyPropertyChanged("Indicated_heading_deg");
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
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
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
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
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
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
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
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
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
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
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
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
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
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
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
                ailrone = value;
                setProperty(value, 3);
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
                setProperty(value, 1);
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
                setProperty(value, 2);
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
                setProperty(value, 0);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }
        public void setProperty(double value, int setter)
        {
            // Creating the set message to the server by dividing into cases
            string send = "";
            switch (setter)
            {
                case 0:
                    {
                        send = "set /controls/engines/current-engine/throttle " + value;
                        break;
                    }
                case 1:
                    {
                        send = "set /controls/flight/elevator " + value;
                        break;
                    }
                case 2:
                    {
                        send = "set /controls/flight/rudder " + value;
                        break;
                    }
                case 3:
                    {
                        send = "set /controls/flight/aileron " + value;
                        break;
                    }
            }
            // Adding the new send request into the list. Using lock because the thread is also using this list.
            lock (balancelock)
            {
                this.msg_to_send.Add(send + " \r\n");
            }
        }
    }
}
