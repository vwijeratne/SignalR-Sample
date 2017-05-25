using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRServiceHub
{
    [HubName("echo")]
    public class SignalRServer : Hub
    {
        public void SendPerformance()
        {
            Clients.All.AddMessage(new ClockModel());
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}