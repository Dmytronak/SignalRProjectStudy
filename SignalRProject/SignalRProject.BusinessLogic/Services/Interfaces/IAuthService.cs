using SignalRProject.ViewModels.AuthViews;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginAuthResponseView> Login(LoginAuthView model);
        Task<LoginAuthResponseView> Register(RegisterAuthView model);
        Task<GetAllAuthView> GetAll();
        Task<GetUserView> GetbyId(string userId);
        Task LogOut();
    }
}
