using Microsoft.EntityFrameworkCore;
using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public class MessageInRoomRepository : BaseRepository<MessageInRoom>, IMessageInRoomRepository
    {
        public MessageInRoomRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<MessageInRoom>> GetByRoomId(Guid roomId)
        {
            var result = await _dbSet
                 .Where(x => x.RoomId == roomId)
                 .Include(x => x.Message)
                 .ToListAsync();

            return result;
        }

        public async Task<List<MessageInRoom>> GetAllRoomsAndMessages()
        {
            var result = await _dbSet
                .Include(x => x.Room)
                .Include(x => x.Message)
                .ToListAsync();

            return result;
        }

    }
}
