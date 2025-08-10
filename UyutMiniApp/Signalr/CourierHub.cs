using Microsoft.AspNetCore.SignalR;

namespace UyutMiniApp.Signalr
{
    public class CourierHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}