using Microsoft.AspNetCore.SignalR;

namespace UyutMiniApp.Signalr
{
    public class OrderProcessHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
