using SignalRProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRProject.DataAccess.Repositories.Interfaces
{
    public interface IUserInRoomRepository : IBaseRepository<UserInRoom>
    {
        Task<List<UserInRoom>> GetByRoomId(Guid roomId);
        Task<List<UserInRoom>> GetByUserId(string userId);
    }
}
