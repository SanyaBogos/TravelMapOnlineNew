using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using nonintanon.Security;
using TravelMap.Models;

namespace TravelMap.Hubs
{
    public class ChatHub : Hub
    {
        private Entities db = new Entities();

        public void Send(string name, string message)
        {
            //var currentUser = WebSecurity.CurrentUserId;
            //string name = db.UserProfiles.First(u => u.UserId == currentUser).FirstName;
            // Call the addNewMessageToPage method to update clients. 
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}