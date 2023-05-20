using EntertenimentManager.Domain.Entities.Lists.Contracts;
using System;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class GamePersonalLists : IPersonalLists
    {
        public List<PersonalList> Create()
        {
            var list = new List<PersonalList>
            {
                new PersonalList("Pra Jogar"),
                new PersonalList("Jogando"),
                new PersonalList("Jogado")
            };

            return list;
        }
    }
}
