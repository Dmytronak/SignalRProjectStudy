using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRProject.DataAccess.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }

        public string Photo { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Guid MessageId { get; set; }
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }

    }
}
