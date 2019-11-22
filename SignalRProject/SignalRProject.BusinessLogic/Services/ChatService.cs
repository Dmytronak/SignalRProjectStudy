using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRProject.BusinessLogic.Providers.Interfaces;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;
using SignalRProject.ViewModels.ChatViewModel;

namespace SignalRProject.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        #region Properties

        private readonly IRoomRepository _roomRepository;
        private readonly IUserInRoomRepository _userInRoomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageInRoomRepository  _messageInRoomRepository;
        private readonly IImageProvider _imageProvider;

        #endregion Properties

        #region Constructor

        public ChatService(IRoomRepository roomRepository, IMessageRepository messageRepository, IImageProvider imageProvider, IUserInRoomRepository userInRoomRepository, IMessageInRoomRepository messageInRoomRepository)
        {
            _roomRepository = roomRepository;
            _userInRoomRepository = userInRoomRepository;
            _messageRepository = messageRepository;
            _imageProvider = imageProvider;
            _messageInRoomRepository = messageInRoomRepository;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<GetAllRoomsChatView> GetAllRooms(string userId)
        {
            GetAllRoomsChatView result = new GetAllRoomsChatView();

            List<UserInRoom> userInRooms = await _userInRoomRepository.GetByUserId(userId);

            if (!userInRooms.Any())
            {
                return result;
            }

            result.Rooms = userInRooms
                .Select(x => new RoomGetAllRoomsChatViewtem() 
                {
                    Id = x.Room.Id,
                    Name = x.Room.Name,
                    Photo = x.Room.Photo
                })
                .ToList();

            return result;
        }

        public async Task<GetRoomChatView> GetRoomById(Guid roomId)
        {
            GetRoomChatView result = new GetRoomChatView();

            Room room = await _roomRepository.GetById(roomId);
            List<UserInRoom> userInRoom = await _userInRoomRepository.GetByRoomId(roomId);
            List<MessageInRoom> messageInRooms = await _messageInRoomRepository.GetByRoomId(roomId);

            if(room == null)
            {
                return result;
            }

            result.Id = room.Id;
            result.Name = room.Name;
            result.Photo = room.Photo;

            result.Users = userInRoom
                .Select(x => new UserGetRoomChatViewItem
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Photo = x.User.Photo
                }).ToList();

            result.Messages = messageInRooms
                .Select(x => new MessageGetRoomChatViewItem
                {
                    Id = x.Message.Id,
                    Text = x.Message.Text

                }).ToList();

            return result;
        }

        public async Task CreateRoom(CreateRoomChatView model,string userId)
        {
            Room room = new Room
            {
                Name = model.Name,
            };

            if (model.Photo != null)
            {
                room.Photo = _imageProvider.ResizeAndSave(model.Photo,Constants.FilePaths.RoomAvatarImages, $"{room.Id}.png",Constants.DefaultIconSizes.MaxWidthOriginalImage,Constants.DefaultIconSizes.MaxHeightOriginalImage);
            }

            UserInRoom userInRoom = new UserInRoom()
            {
                RoomId = room.Id,
                UserId = userId
            };

            await _roomRepository.Create(room);
            await _userInRoomRepository.Create(userInRoom);
        }

        #endregion Public Methods

        #region Privete Methods

        #endregion Privete Methods
    }
}
