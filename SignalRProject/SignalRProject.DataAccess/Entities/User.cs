using Microsoft.AspNetCore.Identity;
using System;

namespace SignalRProject.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int Age { get; set; }
        public Guid CurrentRoomId { get;set; }
    }
}
