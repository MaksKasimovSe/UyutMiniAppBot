using Microsoft.AspNetCore.SignalR;

namespace UyutMiniApp.Signalr
{
    public class OrderCheckHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
