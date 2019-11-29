using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserInRoomRepository _userInRoomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageInRoomRepository  _messageInRoomRepository;
        private readonly IImageProvider _imageProvider;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructor

        public ChatService(UserManager<User> userManager, IMapper mapper, IRoomRepository roomRepository, IMessageRepository messageRepository, IImageProvider imageProvider, IUserInRoomRepository userInRoomRepository, IMessageInRoomRepository messageInRoomRepository)
        {
            _roomRepository = roomRepository;
            _userInRoomRepository = userInRoomRepository;
            _messageRepository = messageRepository;
            _imageProvider = imageProvider;
            _messageInRoomRepository = messageInRoomRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<GetAllRoomsChatView> GetByUserId(string userId)
        {
            GetAllRoomsChatView response = new GetAllRoomsChatView();

            List<UserInRoom> userInRooms = await _userInRoomRepository.GetByUserId(userId);

            if (!userInRooms.Any())
            {
                return response;
            }

            response.Rooms = _mapper.Map(userInRooms, response.Rooms);

            return response;
        }

        public async Task<GetRoomChatView> GetRoomById(Guid roomId)
        {
            GetRoomChatView response = new GetRoomChatView();

            Room room = await _roomRepository.GetById(roomId);
            List<UserInRoom> userInRoom = await _userInRoomRepository.GetByRoomId(roomId);
            List<MessageInRoom> messageInRooms = await _messageInRoomRepository.GetByRoomId(roomId);

            if(room == null)
            {
                return response;
            }

            response = _mapper.Map(room, response);
            response.Users = _mapper.Map(userInRoom, response.Users);
            response.Messages = _mapper.Map(messageInRooms, response.Messages);

            return response;
        }

        public async Task<GetAllMessagesChatView> GetAllMessagesByRoomId(Guid roomId)
        {
            GetAllMessagesChatView response = new GetAllMessagesChatView();

            List<MessageInRoom> messageInRooms = await _messageInRoomRepository.GetByRoomId(roomId);
            if (!messageInRooms.Any())
            {
                return response;
            }

            response.RoomId = roomId;
            response.Messages = _mapper.Map(messageInRooms, response.Messages);

            return response;
        }

        public async Task CreateRoom(CreateRoomChatView model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
          
            Room room = new Room
            {
                Name = model.Name,
            };

            if (model.Photo != null)
            {
                room.Photo = _imageProvider.ResizeAndSave(model.Photo,Constants.FilePaths.RoomAvatarImages, $"{room.Id}.png",Constants.DefaultIconSizes.MaxWidthOriginalImage,Constants.DefaultIconSizes.MaxHeightOriginalImage);
            }

            user.CurrentRoomId = room.Id;

            UserInRoom userInRoom = new UserInRoom()
            {
                RoomId = room.Id,
                UserId = userId
            };

            await _roomRepository.Create(room);
            await _userInRoomRepository.Create(userInRoom);
            await _userManager.UpdateAsync(user);
        }

        public async Task<GetAllRoomsChatView> GetAllRooms()
        {
            GetAllRoomsChatView response = new GetAllRoomsChatView();

            List<Room> rooms = await _roomRepository.GetAll();

            if (!rooms.Any())
            {
                return response;
            }

            response.Rooms = _mapper.Map(rooms, response.Rooms);

            return response;
        }
         
        public async Task<GetUserCurrentRoomChatView> GetUserCurrentRoomByUserId(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User is not find");
            }

            GetUserCurrentRoomChatView response = new GetUserCurrentRoomChatView();
            response = _mapper.Map(user, response);

            return response;

        }

        public async Task SetUserCurrentRoomByUserId(string userId, Guid roomId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User is not find");
            }

            user.CurrentRoomId = roomId;

            await _userManager.UpdateAsync(user);

        }

        public async Task CreateMessage(string messageText, Guid roomId, string userId)
        {
            Message message = new Message 
            { 
                Text = messageText, 
                UserId = userId 
            };

            MessageInRoom messageInRoom = new MessageInRoom
            {
                RoomId = roomId,
                MessageId = message.Id
            };
            await _messageRepository.Create(message);
            await _messageInRoomRepository.Create(messageInRoom);
        }

        #endregion Public Methods

        #region Privete Methods

        #endregion Privete Methods
    }
}
