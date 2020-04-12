﻿using System;
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
        /// <summary>The size</summary>
        private const short Size = 512;

        /// <summary>The client</summary>
        private TcpClient client;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Client()
        {
            this.client = new TcpClient(AddressFamily.InterNetwork);
        }

        public void Connect(string ip, int port)
        {
            this.client.Connect(IPAddress.Parse(ip), port);
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
                NetworkStream networkStream = this.client.GetStream();
                byte[] sendBytes = Encoding.ASCII.GetBytes(data);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
            }
        }

        public void flush()
        {
            this.client.GetStream().Flush();
        }
        public string Read()
        {
            if (this.isConnected())
            {
                NetworkStream ns = this.client.GetStream();
                byte[] dataBytes = new byte[Size];
                int bytesRead = ns.Read(dataBytes, 0, Size);
                string dataToSend = Encoding.ASCII.GetString(dataBytes, 0, bytesRead);
                return dataToSend;
            }
            throw new Exception("Client disconnected, turn FlightGear on and press connect");
        }
    }
}