using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WlToolsLib.TcpUdp
{
    /// <summary>
    /// 
    /// </summary>
    public class TCPServer
    {
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public TcpListener Listener { get; set; }
        public TcpClient Client { get; set; }
        public Encoding Coding { get; set; } = Encoding.UTF8;
        public Action<string> Report { get; set; }
        public int BuffSize { get; set; } = 65_536;
        public TCPServer(int port, string ip = "")
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                this.IP = IPAddress.Any;
            }
            this.Port = port;
            Report = (s) => Console.WriteLine(s);
        }

        public void ServerStart()
        {
            try
            {
                Listener = new TcpListener(IP, Port);//创建TcpListener实例
                Listener.Start();//start
                byte[] bytes = new byte[BuffSize];
                string data = null;
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = Listener.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = Coding.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        byte[] msg = Coding.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                    Console.WriteLine("TCP Server Close");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
            finally
            {
                Listener.Stop();
            }
        }
    }
}
