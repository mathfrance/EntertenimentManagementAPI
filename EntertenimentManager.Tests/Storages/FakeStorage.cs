using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Storages
{
    internal class FakeStorage : IImageStorage
    {
        public Task Upload(byte[] imageBytes, string fileName)
        { return Task.CompletedTask; }
    }
}
