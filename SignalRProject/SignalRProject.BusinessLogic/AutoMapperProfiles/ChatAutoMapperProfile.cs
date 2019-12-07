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

            CreateMap<User, UserGetAllMessagesChatViewItem>()
              .ForMember(destination => destination.Id,
                  options => options.MapFrom(source => source.Id))
              .ForMember(destination => destination.FirstName,
                  options => options.MapFrom(source => source.FirstName))
              .ForMember(destination => destination.LastName,
                  options => options.MapFrom(source => source.LastName))
              .ForMember(destination => destination.Photo,
                  options => options.MapFrom(source => source.Photo));


            CreateMap<MessageInRoom, MessageGetAllMessagesChatViewItem>()
             .ForMember(destination => destination.Id,
                 options => options.MapFrom(source => source.Message.Id))
             .ForMember(destination => destination.UserId, 
                 options => options.MapFrom(source=> source.Message.UserId))
             .ForMember(destination => destination.FullName,
                 options => options.MapFrom(source => $"{source.Message.User.FirstName} {source.Message.User.LastName}"))
             .ForMember(destination => destination.CreationAt,
                 options => options.MapFrom(source => source.Message.CreationAt.ToString("MMMM dd, yyyy H:mm:ss")))
             .ForMember(destination => destination.Text,
                 options => options.MapFrom(source => source.Message.Text));

            CreateMap<List<Room>, GetAllRoomsChatView>()
                 .ForMember(destination => destination.Rooms,
                    options => options.MapFrom(source => source));

            CreateMap<MessageInRoom, RoomGetAllRoomsChatViewtem>()
             .ForMember(destination => destination.Id,
                 options => options.MapFrom(source => source.Room.Id))
             .ForMember(destination => destination.Name,
                 options => options.MapFrom(source => source.Room.Name))
             .ForMember(destination => destination.Photo,
                  options => options.MapFrom(source => source.Room.Photo))
              .ForMember(destination => destination.LastMessage,
                 options => options.MapFrom(source => source.Message.Text));
           

            CreateMap<User, GetUserAndCurrentRoomChatView>();
            CreateMap<User, UserGetAllUsersChatViewViewItem>();

            #endregion
        }
    }
}
