using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRProject.DataAccess.Entities
{
    public class MessageInRoom : BaseEntity
    {
        public Guid RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public Guid MessageId { get; set; }
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }

    }
}
