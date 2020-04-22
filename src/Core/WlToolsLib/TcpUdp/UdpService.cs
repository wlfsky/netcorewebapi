using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WlToolsLib.TcpUdp
{
    /// <summary>
    /// Udp服务
    /// </summary>
    public class UdpService
    {
        private static Socket udpServer;
        public Action<string> WriteLog { get; set; }
        public int ThreadSleep { get; set; } = 200;
        public void StartUdpServer(string ip, int port)
        {
            //1,创建socket
            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Any, port);
            //2,绑定ip跟端口号
            udpServer.Bind(endPoint);

            //3，接收数据
            new Thread(ReceiveMessage) { IsBackground = true }.Start();
            Console.WriteLine($"C# UDP 服务启动...");
            if (WriteLog != null)
            {
                WriteLog("C# UDP 服务启动...");
            }
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                int buffSize = 2048;
                byte[] rcv_data = new byte[buffSize];
                int length = udpServer.ReceiveFrom(rcv_data, ref remoteEndPoint);//这个方法会把数据的来源(ip:port)放到第二个参数上
                string message = Encoding.UTF8.GetString(rcv_data, 0, length);
                Report(remoteEndPoint, message);
                Thread.Sleep(ThreadSleep);
            }
        }

        public void Report(EndPoint endPoint, string msg)
        {
            var ep = endPoint as IPEndPoint;
            if (!string.IsNullOrWhiteSpace(msg))
            {
                if (WriteLog != null)
                {
                    WriteLog(msg);
                }
                else
                {
                    Console.WriteLine("IP ：" + ep.Address.ToString() + "：" + ep.Port + " ====> " + msg);
                }
            }
        }
    }
}
