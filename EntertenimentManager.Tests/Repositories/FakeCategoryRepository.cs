using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Repositories.Contracts;

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

        public Task<Category> GetById(int id)
        {
            if(id == 0)
                return Task.FromResult(new Category("Movies", (int)EnumCategories.Movies, new List<PersonalList>()));
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<Category>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
