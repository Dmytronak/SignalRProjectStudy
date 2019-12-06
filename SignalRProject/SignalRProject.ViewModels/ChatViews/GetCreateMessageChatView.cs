using System;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetCreateMessageChatView
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string CreationAt { get; set; }
        public string Text { get; set; }
    }
}
