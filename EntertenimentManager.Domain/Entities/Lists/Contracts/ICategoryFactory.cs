using EntertenimentManager.Domain.Enumerators;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists.Contracts
{
    public interface ICategoryFactory
    {
        IEnumerable<IPersonalList> Create(EnumCategories type);
    }
}

