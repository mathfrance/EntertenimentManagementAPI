﻿
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

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
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
                        .Include(x => x.Category)
                        .FirstOrDefaultAsync(PersonalListQueries.GetById(id));
        }

        public async Task<bool> IsItemAssociatedWithUserIdAsync(int id, int requestUserId)
        {
            return await _context.Movies
                        .AnyAsync(m => m.Id == id && m.BelongsTo.Category.Owner.Id == requestUserId);
        }

        public async Task<IEnumerable<Movie>> GetAllByPersonalId(int personalListId)
        {
            return await _context
                        .Movies
                        .AsNoTracking()
                        .Where(MovieQueries.GetByPersonalListId(personalListId))
                        .ToListAsync();
        }


        public async Task<bool> IsSwitchBetweenSameTypePersonalLists(int itemId, int newPersonalListCategoryType)
        {
            return await _context.Movies
                        .AnyAsync(m => m.Id == itemId && m.BelongsTo.Category.Type == newPersonalListCategoryType);
        }
    }
}
