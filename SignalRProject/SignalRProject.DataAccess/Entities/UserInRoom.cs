using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRProject.DataAccess.Entities
{
    public class UserInRoom : BaseEntity
    {
        public Guid RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
