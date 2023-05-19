using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class PersonalList : Entity, IPersonalList
    {
        public PersonalList()
        {

        }
        public PersonalList(string title, List<Item> items)
        {
            Title = title;
            Items = items;
        }

        public string Title { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public List<Item> Items { get; private set; }
    }
}