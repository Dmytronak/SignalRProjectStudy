using System;

namespace SignalRProject.DataAccess.Entities
{
    public class Message : BaseEntity
    {
        public DateTime Timestamp { get; set; }

        public string Text { get; set; }
    }
}
