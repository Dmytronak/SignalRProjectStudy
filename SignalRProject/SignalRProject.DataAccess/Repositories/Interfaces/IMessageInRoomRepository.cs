using SignalRProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRProject.DataAccess.Repositories.Interfaces
{
    public interface IMessageInRoomRepository : IBaseRepository<MessageInRoom>
    {
        Task<List<MessageInRoom>> GetByRoomId(Guid roomId);
    }
}
