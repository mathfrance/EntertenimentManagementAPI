using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IImageStorage
    {
        Task Upload(byte[] imageBytes, string fileName);
    }
}
