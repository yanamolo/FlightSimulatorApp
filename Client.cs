using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public class Client : IClient
    {
        private const short Size = 512;

        private TcpClient client;
        private NetworkStream ns;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Client()
        {
            this.client = new TcpClient(AddressFamily.InterNetwork);
        }

        public void Connect(string ip, int port)
        {
            this.client.Connect(IPAddress.Parse(ip), port);
            this.ns = this.client.GetStream();
            this.ns.ReadTimeout = 10000;
        }

        public bool isConnected()
        {
            return this.client.Connected;
        }

        public void Disconnect()
        {
            this.client.Close();
        }

        
        public void Write(string data)
        {
            if (this.isConnected())
            {
                
                byte[] sendBytes = Encoding.ASCII.GetBytes(data);
                ns.Write(sendBytes, 0, sendBytes.Length);
            }
        }

        public void flush()
        {
            this.client.GetStream().Flush();
        }
        public string Read()
        {
            try
            {

                if (this.isConnected())
                {
                    byte[] dataBytes = new byte[Size];
                    int bytesRead = ns.Read(dataBytes, 0, Size);
                    string dataToSend = Encoding.ASCII.GetString(dataBytes, 0, bytesRead);
                    return dataToSend;
                }
            }
            catch
            {
                throw new TimeoutException("Server is a bit slow");
            }
            return null;
        }
    }
}