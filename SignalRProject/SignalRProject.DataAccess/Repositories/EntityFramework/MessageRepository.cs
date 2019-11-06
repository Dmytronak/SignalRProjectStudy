using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
