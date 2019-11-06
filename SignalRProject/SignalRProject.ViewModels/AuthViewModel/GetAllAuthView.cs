using System.Collections.Generic;

namespace SignalRProject.ViewModels.AuthViewModel
{
    public class GetAllAuthView
    {
        public List<UserGetAllAuthViewItem> Users { get; set; }

        public GetAllAuthView()
        {
            Users = new List<UserGetAllAuthViewItem>();
        }
    }
    public class UserGetAllAuthViewItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}