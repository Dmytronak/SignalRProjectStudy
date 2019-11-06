using SignalRProject.DataAccess.Entities;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Providers.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> GenerateJwtToken(User user);
    }
}
