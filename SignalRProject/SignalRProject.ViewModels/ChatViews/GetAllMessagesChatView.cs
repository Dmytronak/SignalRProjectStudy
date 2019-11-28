using System;
using System.Collections.Generic;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetAllMessagesChatView
    {
        public Guid RoomId { get; set; }

        public List<MessageGetAllMessagesChatViewItem> Messages {get;set;}
    }

    public class MessageGetAllMessagesChatViewItem
    {
        public Guid Id { get; set; }
        public DateTime CreationAt { get; set; }
        public string Text { get; set; }
    }
}
