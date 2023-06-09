﻿using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Storages
{
    public class FakeStorage : IImageStorage
    {
        public Task UploadAsync(byte[] imageBytes, string fileName)
        { return Task.CompletedTask; }
    }
}
