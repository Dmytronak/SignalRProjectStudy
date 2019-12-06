using System;
using System.Collections.Generic;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetAllMessagesChatView
    {
       
        public Guid RoomId { get; set; }

        public List<UserGetAllMessagesChatViewItem> Users { get; set; }
        public List<MessageGetAllMessagesChatViewItem> Messages {get;set;}

        public GetAllMessagesChatView()
        {
            Messages = new List<MessageGetAllMessagesChatViewItem>();
            Users = new List<UserGetAllMessagesChatViewItem>();
        }

    }

    public class UserGetAllMessagesChatViewItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }

    public class MessageGetAllMessagesChatViewItem
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string CreationAt { get; set; }
        public string Text { get; set; }
    }
}
