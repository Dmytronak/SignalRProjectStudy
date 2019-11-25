using AutoMapper;
using SignalRProject.ViewModels.ChatViews;
using SignalRProject.ViewModels.MenuViewModel;

namespace SignalRProject.BusinessLogic.AutoMapperProfiles
{
    public class MenuAutoMapperProfile : Profile
    {
        public MenuAutoMapperProfile() : base()
        {

            #region From ViewModel to ViewModel
            CreateMap<GetAllRoomsChatView, ChatMenuView>()
               .ForMember(destination => destination.Items,
                    options => options.MapFrom(source => source.Rooms));
            CreateMap<RoomGetAllRoomsChatViewtem, MenuChatViewItem>();
            #endregion From ViewModel to ViewModel

        }
    }
}
