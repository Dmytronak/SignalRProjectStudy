using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IMessageRepository _messageRepository;

        #endregion Properties

        #region Constructor

        public ChatService(IRoomRepository roomRepository, IMessageRepository messageRepository)
        {
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<GetAllRoomsChatView> GetAllRooms()
        {
            var result = new GetAllRoomsChatView();

            var rooms = await _roomRepository.GetAll();

            if (!rooms.Any())
            {
                return result;
            }

            result.Rooms = rooms
                .Select(x => new RoomGetAllRoomsChatViewtem() 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Photo = x.Photo,
                    UserId = x.UserId
                })
                .ToList();

            return result;
        }

        public async Task CreateRoom(CreateRoomChatView model)
        {
            var room = new Room
            {
                Name = model.Name,
                Photo = model.Photo,
                UserId = model.UserId
            };

            await _roomRepository.Create(room);
        }

        #endregion Public Methods

        #region Privete Methods

        #endregion Privete Methods
    }
}
