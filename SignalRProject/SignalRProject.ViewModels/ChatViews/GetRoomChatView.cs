using System;
using System.Collections.Generic;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetRoomChatView
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public List<UserGetRoomChatViewItem> Users { get; set; }

        public List<MessageGetRoomChatViewItem> Messages { get; set; }

        public GetRoomChatView()
        {
            Users = new List<UserGetRoomChatViewItem>();
            Messages = new List<MessageGetRoomChatViewItem>();
        }
    }

    public class UserGetRoomChatViewItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }

    public class MessageGetRoomChatViewItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }

}
