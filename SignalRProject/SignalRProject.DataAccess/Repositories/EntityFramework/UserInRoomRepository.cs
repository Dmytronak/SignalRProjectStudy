using Microsoft.EntityFrameworkCore;
using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class UserInRoomRepository : BaseRepository<UserInRoom>, IUserInRoomRepository
    {
        public UserInRoomRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<UserInRoom>> GetByRoomId(Guid roomId)
        {
            var result = await _dbSet
                 .Where(x => x.RoomId == roomId)
                 .Include(x => x.User)
                 .Include(x => x.Room)
                 .ToListAsync();

            return result;
        }

        public async Task<List<UserInRoom>> GetByUserId(string userId)
        {
            var result = await _dbSet
                 .Where(x => x.UserId == userId)
                 .Include(x => x.Room)
                 .ToListAsync();

            return result;
        }
    }
}
