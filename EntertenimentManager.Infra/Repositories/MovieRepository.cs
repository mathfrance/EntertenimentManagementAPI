
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EntertenimentManager.Infra.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly EntertenimentManagementDataContext _context;
        public MovieRepository(EntertenimentManagementDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<PersonalList?> GetPersonalListById(int id)
        {
            return await _context
                        .PersonalLists
                        .Include(x => (IEnumerable<Movie>)x.Items)
                        .FirstOrDefaultAsync(PersonalListQueries.GetById(id));
        }
    }
}
