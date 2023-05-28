
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Repositories.Contracts
{
    public interface IMovieRepository
    {
        Task CreateAsync(Movie movie);
        //Task UpdateAsync(User user);
        //Task DeleteAsync(User user);
        Task<PersonalList> GetPersonalListById(int id);
        //Task<User> GetByEmailNoTracking(string email);
    }
}
