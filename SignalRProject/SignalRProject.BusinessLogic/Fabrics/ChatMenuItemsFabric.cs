using AutoMapper;
using SignalRProject.BusinessLogic.Fabrics.Interfaces;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.DataAccess.Repositories.Interfaces;
using SignalRProject.ViewModels.ChatViews;
using SignalRProject.ViewModels.MenuViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Fabrics
{

    public class ChatMenuItemsFabric : IChatMenuItemsFabric
    {
        #region Properties
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;
        private readonly IUserInRoomRepository _userInRoomRepository;

        #endregion Properties

        #region Constructor
        public ChatMenuItemsFabric(IMapper mapper, IChatService chatService, IUserInRoomRepository userInRoomRepository)
        {
            _chatService = chatService;
            _mapper = mapper;
            _userInRoomRepository = userInRoomRepository;
        }
        #endregion Constructor

        #region Public Methods

        public ChatMenuView BuildChatMenu()
        {
            ChatMenuView result = new ChatMenuView();

            GetAllRoomsChatView rooms = Task
                .Run(async () => await _chatService.GetAllRooms())
                .Result;

            if (!rooms.Rooms.Any()) 
            {
                return result;
            }
            result = _mapper.Map(rooms, result);

            return result;
        }

        #endregion Public Methods

    }
}
