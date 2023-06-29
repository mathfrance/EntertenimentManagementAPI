using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakeMovieRepository : IMovieRepository
    {
        private readonly IEnumerable<Movie> _movieLists;
        public FakeMovieRepository()
        {
            _movieLists = new List<Movie>();
        }
        public Task CreateAsync(Movie movie)
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Movie movie)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Movie movie)
        {
            return Task.CompletedTask;
        }

        public Task<Movie> GetById(int id)
        {
            if (id == 0)
            {
                return Task.FromResult(new Movie());
            }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<Movie>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        }

        public Task<PersonalList> GetPersonalListById(int id)
        {
            if (id == 0)
            {
                var personalList = new PersonalList("Para Assistir");
                var category = new Category("Movies", (int)EnumCategories.Movies, new List<PersonalList>());
                personalList.AddCategory(category);
                return Task.FromResult(personalList);
            }
            if (id == 1)
            {
                var personalList = new PersonalList("Para Jogar");
                var category = new Category("Games", (int)EnumCategories.Games, new List<PersonalList>());
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

        public Task<IEnumerable<Movie>> GetAllByPersonalId(int personalListId)
        {
            return Task.FromResult(_movieLists);
        }

        public Task<bool> IsSwitchBetweenSameTypePersonalLists(int itemId, int newPersonalListCategoryType)
        {
            if (newPersonalListCategoryType == (int)EnumCategories.Movies) return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}