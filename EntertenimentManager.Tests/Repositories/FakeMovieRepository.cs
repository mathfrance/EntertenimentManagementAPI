using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
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
                return Task.FromResult(new PersonalList("Para Assistir"));
            }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.FromResult<PersonalList>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        }

        public Task<bool> IsItemAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            if (requestUserId == -1) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Movie>> GetAllByPersonalId(int personalListId)
        {
            return Task.FromResult(_movieLists);
        }

        public Task<bool> IsSwitchBetweenSameTypePersonalLists(int id, int requestUserId)
        {
            throw new NotImplementedException();
        }
    }
}