using EntertenimentManager.Domain.Entities.Itens;
using System;
using System.Linq.Expressions;
namespace EntertenimentManager.Domain.Queries
{
    public class MovieQueries
    {
        public static Expression<Func<Movie, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}
