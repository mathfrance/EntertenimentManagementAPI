using EntertenimentManager.Domain.Entities.Lists.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class MoviePersonalLists : IPersonalLists
    {
        public List<PersonalList> Create()
        {
            var list = new List<PersonalList>
            {
                new PersonalList("Pra Assistir"),
                new PersonalList("Assistido")
            };

            return list;
        }
    }
}
