using AutoMapper;
using SignalRProject.DataAccess.Entities;
using SignalRProject.ViewModels.ChatViews;
using System.Collections.Generic;
using System.Linq;

namespace SignalRProject.BusinessLogic.AutoMapperProfiles
{
    public class ChatAutoMapperProfile : Profile
    {
        public ChatAutoMapperProfile() : base()
        {
            #region From ViewModel to Model

            #endregion

            #region From Model to ViewModel
            CreateMap<List<UserInRoom>, GetAllRoomsChatView>()
               .ForMember(destination => destination.Rooms,
                    options => options.MapFrom(source => source));
            CreateMap<UserInRoom, RoomGetAllRoomsChatViewtem>()
                .ForMember(destination => destination.Id,
                    options => options.MapFrom(source => source.Room.Id))
                .ForMember(destination => destination.Name,
                    options => options.MapFrom(source => source.Room.Name))
                .ForMember(destination => destination.Photo,
                    options => options.MapFrom(source => source.Room.Photo));

            CreateMap<Room, GetRoomChatView>();
            CreateMap<UserInRoom, UserGetRoomChatViewItem>()
              .ForMember(destination => destination.Id,
                  options => options.MapFrom(source => source.User.Id))
              .ForMember(destination => destination.FirstName,
                  options => options.MapFrom(source => source.User.FirstName))
              .ForMember(destination => destination.LastName,
                  options => options.MapFrom(source => source.User.LastName))
              .ForMember(destination => destination.Photo,
                  options => options.MapFrom(source => source.User.Photo));
            CreateMap<MessageInRoom, MessageGetRoomChatViewItem>()
               .ForMember(destination => destination.Id,
                   options => options.MapFrom(source => source.Message.Id))
               .ForMember(destination => destination.Text,
                   options => options.MapFrom(source => source.Message.Text));

            CreateMap<MessageInRoom, MessageGetAllMessagesChatViewItem>()
             .ForMember(destination => destination.Id,
                 options => options.MapFrom(source => source.Message.Id))
               .ForMember(destination => destination.CreationAt,
                 options => options.MapFrom(source => source.Message.CreationAt))
             .ForMember(destination => destination.Text,
                 options => options.MapFrom(source => source.Message.Text));

            #endregion
        }
    }
}
