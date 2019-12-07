using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SignalRProject.BusinessLogic.Helpers.Interfaces;
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
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion Properties

        #region Constructor

        public ChatService(UserManager<User> userManager, IDateTimeHelper dateTimeHelper, IMapper mapper, 
            IRoomRepository roomRepository, IMessageRepository messageRepository, IImageProvider imageProvider, IUserInRoomRepository userInRoomRepository, IMessageInRoomRepository messageInRoomRepository)
        {
            _roomRepository = roomRepository;
            _userInRoomRepository = userInRoomRepository;
            _messageRepository = messageRepository;
            _imageProvider = imageProvider;
            _messageInRoomRepository = messageInRoomRepository;
            _mapper = mapper;
            _userManager = userManager;
            _dateTimeHelper = dateTimeHelper;
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

        public async Task<GetAllMessagesChatView> GetAllMessagesByRoomId(Guid roomId)
        {
            GetAllMessagesChatView response = new GetAllMessagesChatView();
            List<MessageInRoom> messageInRooms = await _messageInRoomRepository.GetByRoomId(roomId);
            List<MessageInRoom> orderedMessageInRooms = messageInRooms
                .OrderBy(x => x.Message.CreationAt)
                .ToList();
            List<User> usersInRoom = _userManager.Users
                .Where(x => x.CurrentRoomId == roomId)
                .ToList();

            if (messageInRooms.Where(x => x.Message.User == null).Any())
            {
                foreach (var item in messageInRooms)
                {
                    User user = await _userManager.FindByIdAsync(item.Message.UserId);
                    item.Message.User = user;
                }

            }
       
            response.RoomId = roomId;
            response.Users = _mapper.Map(usersInRoom, response.Users);
            response.Messages = _mapper.Map(orderedMessageInRooms, response.Messages);

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
            List<MessageInRoom> lastMessages = new List<MessageInRoom>();
            List<Room> rooms = await _roomRepository.GetAll();
            List<MessageInRoom> messageInRooms = await _messageInRoomRepository.GetAllRoomsAndMessages();
            messageInRooms
                .GroupBy(x => x.RoomId)
                .ToList()
                .ForEach(item => 
                {
                    MessageInRoom lastMessage = item
                        .Where(x => x.RoomId == item.Key)
                        .OrderByDescending(x => x.CreationAt)
                        .FirstOrDefault();
                    lastMessages.Add(lastMessage);
                });

          
            if (!messageInRooms.Any())
            {
                return response;
            }

            response.Rooms = _mapper.Map(lastMessages, response.Rooms);

            if (rooms.Count > response.Rooms.Count)
            {
                List<Guid> roomsWithMessagesIds = response.Rooms
                    .Select(x => x.Id)
                    .ToList();

                var roomsWithotMessages = rooms
                    .Where(x => !roomsWithMessagesIds
                    .Contains(x.Id))
                    .ToList()
                    .Select(x=> new RoomGetAllRoomsChatViewtem() 
                    { 
                        Id = x.Id,
                        Name = x.Name,
                        Photo = x.Photo,
                        LastMessage = "No messages yet"
                    })
                    .ToList();

                response.Rooms.AddRange(roomsWithotMessages);
            }
            
            return response;
        }
         
        public async Task<GetUserAndCurrentRoomChatView> GetUserAndCurrentRoomByUserId(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User is not find");
            }

            GetUserAndCurrentRoomChatView response = new GetUserAndCurrentRoomChatView();
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

        public async Task<GetCreateMessageChatView> CreateMessage(string messageText, Guid roomId, string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

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
            GetCreateMessageChatView response = new GetCreateMessageChatView()
            {
                Id = message.Id,
                CreationAt = _dateTimeHelper.FormatDateFromDb(message.CreationAt),
                Text = message.Text,
                FullName = $"{user.FirstName} {user.LastName}",
                UserId = userId
            };

            await _messageRepository.Create(message);
            await _messageInRoomRepository.Create(messageInRoom);

            return response;
        }

        public GetAllUsersChatView GetAllUsersByRoomId(Guid roomId)
        {
            var users = _userManager.Users
                .Where(x => x.CurrentRoomId == roomId)
                .ToList();
            var response = new GetAllUsersChatView();

            response.Users = _mapper.Map(users, response.Users);
            return response;
        }

        #endregion Public Methods

        #region Privete Methods

        #endregion Privete Methods
    }
}
