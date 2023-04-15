using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Storages
{
    internal class FakeStorage : IImageStorage
    {
        public void Upload(byte[] imageBytes, string fileName)
        { }
    }
}
