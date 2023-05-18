using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Entities.Lists.Contracts
{
    public interface ICategory<T>
    {
        List<T> PersonalLists { get; }
    }
}
