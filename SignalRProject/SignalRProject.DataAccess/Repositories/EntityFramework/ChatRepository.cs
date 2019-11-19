using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class ChatRepository : BaseRepository<Room>, IChatRepository
    {
        public ChatRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
