using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRProject.DataAccess.Entities;

namespace SignalRProject.DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserInRoom> UserInRooms { get; set; }
        public DbSet<MessageInRoom> MessageInRooms { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
        }
    }
}
