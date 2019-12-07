using System;
using System.Collections.Generic;

namespace SignalRProject.ViewModels.MenuViewModel
{
    public class ChatMenuView
    {
        public List<MenuChatViewItem> Items { get; set; }

        public ChatMenuView()
        {
            Items = new List<MenuChatViewItem>();
        }
    }

    public class MenuChatViewItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public string LastMessage { get; set; }
    }
}

