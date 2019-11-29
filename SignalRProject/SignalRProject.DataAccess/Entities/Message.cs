using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRProject.DataAccess.Entities
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
