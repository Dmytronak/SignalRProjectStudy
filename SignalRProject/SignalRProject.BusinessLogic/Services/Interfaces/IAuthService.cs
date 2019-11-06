using SignalRProject.ViewModels.AuthViewModel;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginAuthResponseView> Login(LoginAuthView model);
        Task<LoginAuthResponseView> Register(RegisterAuthView model);
        Task<GetAllAuthView> GetAll();
    }
}
