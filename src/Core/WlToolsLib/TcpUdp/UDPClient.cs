using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WlToolsLib.TcpUdp
{
    /// <summary>
    /// udp客户端功能
    /// </summary>
    public static class UdpClient
    {
        /// <summary>
        /// UDP发送
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="encoding"></param>
        /// <param name="beforSend"></param>
        /// <param name="afterSend"></param>
        public static void UdpSend(this string msg, string ip, int port, Encoding encoding = null, Func<string, string> beforSend = null, Action<string> afterSend = null)
        {
            var sendList = new List<string>() { msg };
            sendList.UdpSend(ip, port, encoding, beforSend, afterSend);
        }

        /// <summary>
        /// UDP发送 自动关闭
        /// </summary>
        /// <param name="msgs"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="encoding"></param>
        /// <param name="beforSend"></param>
        /// <param name="afterSend"></param>
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
