using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Categories.Contracts
{
    public interface ICategoryFactory
    {
        IEnumerable<PersonalList> Create(EnumCategories type);
    }
}