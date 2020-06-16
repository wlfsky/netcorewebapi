using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WL.Core.WebApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            for (int i = 0; i < 10; i++)
            {
                await Clients.All.SendAsync("ReceiveMessage", $"ser_u：{user}.{i}", $"ser_m：{message}.{i}");
            }
        }
    }
}
