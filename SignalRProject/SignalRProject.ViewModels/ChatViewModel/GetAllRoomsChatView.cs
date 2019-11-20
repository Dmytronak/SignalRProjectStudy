using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRProject.ViewModels.ChatViewModel
{
    public class GetAllRoomsChatView
    {
        public List<RoomGetAllRoomsChatViewtem> Rooms { get; set; }

        public GetAllRoomsChatView()
        {
            Rooms = new List<RoomGetAllRoomsChatViewtem>();
        }
    }

    public class RoomGetAllRoomsChatViewtem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public string UserId { get; set; }
    }
}
