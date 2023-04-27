using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IImageStorage
    {
        Task UploadAsync(byte[] imageBytes, string fileName);
    }
}
