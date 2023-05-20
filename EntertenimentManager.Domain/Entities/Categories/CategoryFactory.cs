using EntertenimentManager.Domain.Entities.Categories.Contracts;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Categories
{
    public class CategoryFactory : ICategoryFactory
    {
        public IEnumerable<PersonalList> Create(EnumCategories type)
        {
            switch (type)
            {
                case EnumCategories.Movies:
                    var movieLists = new MoviePersonalLists();
                    return movieLists.Create();
                case EnumCategories.Games:
                    var gameLists = new GamePersonalLists();
                    return gameLists.Create();
                default:
                    return Enumerable.Empty<PersonalList>();
            }
        }
    }
}