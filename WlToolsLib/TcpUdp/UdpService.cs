using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WlToolsLib.TcpUdp
{
    public static class UdpService
    {
        public static void UdpSend(this string msg, string ip, int port, Encoding encoding = null, Func<string, string> beforSend = null, Action<string> afterSend = null)
        {
            var sendList = new List<string>() { msg };
            sendList.UdpSend(ip, port, encoding, beforSend, afterSend);
        }


        public static void UdpSend(this IEnumerable<string> msgs, string ip, int port, Encoding encoding = null, Func<string, string> beforSend = null, Action<string> afterSend = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                EndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
                foreach (var msg in msgs)
                {
                    var tempMsg = msg;
                    if (beforSend != null) { tempMsg = beforSend(msg); }
                    server.SendTo(encoding.GetBytes(tempMsg), point);
                    if (afterSend != null) { afterSend(tempMsg); }
                }
            }
        }


    }
}
