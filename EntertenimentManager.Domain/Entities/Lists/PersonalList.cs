using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class PersonalList : Entity
    {
        public PersonalList(string title)
        {
            Title = title;
            Items = new();
        }

        public string Title { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public List<Item> Items { get; private set; }

        public void AddCategory (Category category)
        {
            Category ??= category;
        }
    }
}