using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, ActiveUserList> ActiveUsers = new Dictionary<string, ActiveUserList>();
        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user,message);
        }
        public async Task SendToUser(string user, string receiverConnectionId, string message)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, message);
        }
        public string GetConnectionId() => Context.ConnectionId;
        public string AddConnectionIdToList(string id, string name)
        {
            ActiveUsers.Add(id, new ActiveUserList() { UserId = id, UserName = name });
            return "success";
        }
    }
}
