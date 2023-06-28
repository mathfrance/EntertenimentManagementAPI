
using EntertenimentManager.Domain.Entities.Itens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IMovieRepository : IItemRepository
    {
        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);        
        Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetAllByPersonalId(int personalListId);
    }
}
