namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IImageStorage
    {
        void Upload(byte[] imageBytes, string fileName);
    }
}
