using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace IMChat.Hubs
{
    public class ChatHub : Hub
    {
        //public Task OnConnected()
        //{
        //    return base.OnConnected();
        //}

        public void Send(string name, string message)
        {
            //广播
            Clients.All.addMessage(name, message);
        }

        public void SendTo(string name, string message, string sendname)
        {
            //Dictionary<string, string> _clients = new Dictionary<string, string>();
            //if (!_clients.ContainsKey(name))
            //    _clients.Add(name, Context.ConnectionId);
            //Clients.Client(_clients[name]).addTo(message);

            Clients.All.addTo(name, message, sendname);

        }
    }
}