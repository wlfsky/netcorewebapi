using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WlToolsLib.TcpUdp
{
    /// <summary>
    /// 
    /// </summary>
    public class TCPClient
    {
        public TcpClient Client { get; set; }
        public IPEndPoint IPep { get; set; }
        public IPAddress IPAddr { get; set; }
        public int Port { get; set; }
        public Encoding Coding { get; set; } = Encoding.UTF8;
        public Action<string> Report { get; set; }
        public int BuffSize { get; set; } = 65_535;
        public TCPClient(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                throw new FormatException($"{ip} 是空的IP地址");
            }
            IPAddress ipAddr = null;
            if (System.Net.IPAddress.TryParse(ip, out ipAddr))
            {
                IPAddr = ipAddr;
            }
            else
            {
                throw new FormatException($"{ip} 不是有效的IP地址");
            }
            Port = port;
            IPep = new IPEndPoint(IPAddr, Port);
            Report = (s) => Console.WriteLine(s);
        }
        public void Start()
        {

        }
        public void StartAndSend(string msg)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                //Client = new TcpClient(IPep);
                Client = new TcpClient("127.0.0.1", 58884);

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = Coding.GetBytes(msg);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = Client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Report($"Sent: {msg}");

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new byte[BuffSize];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Coding.GetString(data, 0, bytes);
                Report($"Received: {responseData}");

                // Close everything.
                stream.Close();
                Client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void StartAndForeachSend(Func<string> sendFn)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                //Client = new TcpClient(IPep);
                Client = new TcpClient("127.0.0.1", 58884);

                var msg = sendFn();

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = Coding.GetBytes(msg);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = Client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Report($"Sent: {msg}");

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new byte[BuffSize];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Coding.GetString(data, 0, bytes);
                Report($"Received: {responseData}");

                // Close everything.
                stream.Close();
                Client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
