using System.IO;
using System.Threading.Tasks;

namespace SlaezyBookingEventApp.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream imageStream, string imageName);
        Task<bool> DeleteImageAsync(string imageUrl); // Add this line
        // You might add other image-related methods here, like deleting images
    }
}