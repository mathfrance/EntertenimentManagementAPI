using EntertenimentManager.Domain.Entities.Categories;
using System;
using System.Linq.Expressions;

namespace EntertenimentManager.Domain.Queries
{
    public static class CategoryQueries
    {
        public static Expression<Func<Category, bool>> GetByUserId(int id)
        {
            return x => x.Owner.Id == id;
        }
    }
}
