using EntertenimentManager.Domain.Entities.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Entities.Lists.Contracts
{
    public interface IPersonalLists
    {
        List<PersonalList> Create();
    }
}