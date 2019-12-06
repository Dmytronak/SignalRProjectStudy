using System.Collections.Generic;

namespace SignalRProject.ViewModels.ChatViews
{
    public class GetAllUsersChatView
    {
        public List<UserGetAllMessagesChatViewItem> Users { get; set; }

        public GetAllUsersChatView()
        {
            Users = new List<UserGetAllMessagesChatViewItem>();
        }

    }

    public class UserGetAllUsersChatViewViewItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }

}
