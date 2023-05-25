using EntertenimentManager.Domain.Entities.Categories;
using System.Linq.Expressions;
using System;
using EntertenimentManager.Domain.Entities.Lists;

namespace EntertenimentManager.Domain.Queries
{
    public class PersonalListQueries
    {
        public static Expression<Func<PersonalList, bool>> GetByCategoryId(int categoryId)
        {
            return x => x.Category.Id == categoryId;
        }

        public static Expression<Func<PersonalList, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}
