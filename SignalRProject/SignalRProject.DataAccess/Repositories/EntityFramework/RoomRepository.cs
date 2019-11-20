using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
