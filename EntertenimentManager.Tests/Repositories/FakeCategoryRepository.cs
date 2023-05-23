using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Repositories.Contracts;
using System.Collections.Generic;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        private readonly IEnumerable<Category> _categoryLists;
        public FakeCategoryRepository()
        {
            _categoryLists = new List<Category>();
        }
        public Task<IEnumerable<Category>>? GetAllByUserId(int userId)
        {
            return Task.FromResult(_categoryLists);
        }
    }
}
