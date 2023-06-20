
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            return await _context
                        .Movies
                        .FirstOrDefaultAsync(MovieQueries.GetById(id));
        }

        public async Task<PersonalList?> GetPersonalListById(int id)
        {
            return await _context
                        .PersonalLists
                        .FirstOrDefaultAsync(PersonalListQueries.GetById(id));
        }

        public async Task<bool> IsMovieAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            return await _context.Movies
                        .AnyAsync(m => m.Id == id && m.BelongsTo.Category.Owner.Id == requestUserId);
        }
    }
}
