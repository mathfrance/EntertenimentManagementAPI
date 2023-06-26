
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<PersonalList> GetPersonalListById(int id);
        Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetAllByPersonalId(int personalListId);

        Task<bool> IsMovieAssociatedWithUserIdAsync(int id, int requestUserId);
    }
}
