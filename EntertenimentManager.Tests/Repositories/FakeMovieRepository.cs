using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Repositories.Contracts;

namespace EntertenimentManager.Tests.Repositories
{
    public class FakeMovieRepository : IMovieRepository
    {
        public Task CreateAsync(Movie movie)
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Movie movie)
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

        public Task<bool> IsMovieAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            if (requestUserId == -1) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}