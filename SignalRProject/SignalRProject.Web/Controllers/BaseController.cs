using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SignalRProject.Web.Controllers
{
    public class BaseController : Controller
    {
        public string UserId { get { return HttpContext?.User?.Claims?.ToList()?.FirstOrDefault()?.Value ?? string.Empty; } }
    }
}
