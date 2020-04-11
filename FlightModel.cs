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
        //Propreties
        private string errors;

        private string latitude_deg;
        private string longitude_deg;
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

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
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

                        telnetClient.Write("get /position/latitude-deg\r\n");
                        latitude_deg = telnetClient.Read();
                        telnetClient.Write("get /position/longitude-deg\r\n");
                        longitude_deg = telnetClient.Read();
                        latitude_deg = latitude_deg.Substring(0, latitude_deg.Length - 1);
                        longitude_deg = longitude_deg.Substring(0, longitude_deg.Length - 1);
                        Coardinates = latitude_deg + "," + longitude_deg;

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
                        Thread.Sleep(250);// read the data in 4Hz 
                    }
                    catch
                    {
                        this.disconnect();
                        Errors = "Server Disconnected";
                    }
                }
            }).Start();
        }
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
            lock (balancelock)
            {
                this.msg_to_send.Add(send + " \r\n");
            }
        }
    }
}
