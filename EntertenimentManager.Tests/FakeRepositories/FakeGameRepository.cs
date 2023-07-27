using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.FakeRepositories
{
    public class FakeGameRepository : IGameRepository
    {
        private readonly IEnumerable<Game> _gameLists;
        private readonly IEnumerable<Platform> _platformsLists;
        public FakeGameRepository()
        {
            _gameLists = new List<Game>();
            _platformsLists = new List<Platform>();
        }
        public Task CreateAsync(Game game)
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Game game)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Game game)
        {
            return Task.CompletedTask;
        }

        public Task<Game> GetById(int id)
        {
            if (id == 0)
            {
                return Task.FromResult(new Game());
            }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<Game>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        }

        public Task<PersonalList> GetPersonalListById(int id)
        {
            if (id == 0)
            {
                var personalList = new PersonalList("Jogando");
                var category = new Category("Games", (int)EnumCategories.Games, new List<PersonalList>());
                personalList.AddCategory(category);
                return Task.FromResult(personalList);
            }
            if (id == 1)
            {
                var personalList = new PersonalList("Para Assistir");
                var category = new Category("Movies", (int)EnumCategories.Movies, new List<PersonalList>());
                personalList.AddCategory(category);
                return Task.FromResult(personalList);
            }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<PersonalList>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        }

        public Task<bool> IsItemAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            if (id == -1 && requestUserId == -1) return Task.FromResult(true);

            if (requestUserId == -1) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Game>> GetAllByPersonalId(int personalListId)
        {
            return Task.FromResult(_gameLists);
        }

        public Task<bool> IsSwitchBetweenSameTypePersonalLists(int itemId, int newPersonalListCategoryType)
        {
            if (newPersonalListCategoryType == (int)EnumCategories.Games) return Task.FromResult(true);

            return Task.FromResult(false);
        }

        public Task<IEnumerable<Platform>> GetPlatformsByIds(IEnumerable<int> platformsIds)
        {
            return Task.FromResult(_platformsLists);
        }
    }
}
