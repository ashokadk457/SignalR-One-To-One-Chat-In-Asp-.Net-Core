using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace SignalRDemo.Chat
{
    [AllowAnonymous]
    public class ChatingHub : Hub
    {
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Debug.WriteLine("Client disconnected: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("Client connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        //Create Group for each user to chat sepeartely
        public void SetUserChatGroup(string userChatId)
        {
            var id = Context.ConnectionId;
            Debug.WriteLine($"Client {id} added to group " + userChatId);
            Groups.AddToGroupAsync(Context.ConnectionId, userChatId);
        }
        //Send message to user Group
        public async Task SendMessageToGroup(string senderChatId,string senderName, string receiverChatId, string message)
        {
            await Clients.Group(receiverChatId).SendAsync("ReceiveMessage", senderChatId, senderName, receiverChatId, message);
        }
    }
}
