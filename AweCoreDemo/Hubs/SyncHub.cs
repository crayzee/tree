using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AweCoreDemo.Hubs
{
    public class SyncHub : Hub
    {
        public async Task Send(string key, string act, string group)
        {
            await Clients.Others.SendAsync("ReceiveMessage", key, act, group);
        }
    }
}