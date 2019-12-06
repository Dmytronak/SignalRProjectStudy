using System;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetUserAndCurrentRoomChatView
    {
        public string Id { get; set; }
        public Guid CurrentRoomId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }
}
