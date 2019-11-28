using Microsoft.AspNetCore.Http;

namespace SignalRProject.BusinessLogic.Providers.Interfaces
{
    public interface IImageProvider
    {
        string ResizeAndSave(IFormFile file, string path, string name, int? maxWeight = null, int? maxHeight = null);
    }
}
