using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
