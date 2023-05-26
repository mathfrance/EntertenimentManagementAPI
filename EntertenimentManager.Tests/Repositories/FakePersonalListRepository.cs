using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakePersonalListRepository : IPersonalListRepository
    {
        private readonly IEnumerable<PersonalList> _personalLists;
        public FakePersonalListRepository()
        {
            _personalLists = new List<PersonalList>();
        }

        public Task<IEnumerable<PersonalList>> GetAllByCategoryId(int categoryId)
        {
            return Task.FromResult(_personalLists);
        }

        Task<PersonalList> IPersonalListRepository.GetById(int id)
        {
            if (id == 0)
                return Task.FromResult(new PersonalList("Assistido"));
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<PersonalList>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
