using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SignalRProject.BusinessLogic.Providers.Interfaces;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;
using SignalRProject.ViewModels.ChatViews;

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
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructor

        public ChatService(IMapper mapper, IRoomRepository roomRepository, IMessageRepository messageRepository, IImageProvider imageProvider, IUserInRoomRepository userInRoomRepository, IMessageInRoomRepository messageInRoomRepository)
        {
            _roomRepository = roomRepository;
            _userInRoomRepository = userInRoomRepository;
            _messageRepository = messageRepository;
            _imageProvider = imageProvider;
            _messageInRoomRepository = messageInRoomRepository;
            _mapper = mapper;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<GetAllRoomsChatView> GetByUserId(string userId)
        {
            GetAllRoomsChatView result = new GetAllRoomsChatView();

            List<UserInRoom> userInRooms = await _userInRoomRepository.GetByUserId(userId);

            if (!userInRooms.Any())
            {
                return result;
            }

            result.Rooms = _mapper.Map(userInRooms, result.Rooms);

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

            result = _mapper.Map(room, result);
            result.Users = _mapper.Map(userInRoom, result.Users);
            result.Messages = _mapper.Map(messageInRooms, result.Messages);
            if (!result.Messages.Any())
            {
                result.Messages.Add(new MessageGetRoomChatViewItem { Id = Guid.NewGuid(), Text = "ERRRROORRR" });
            }

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
