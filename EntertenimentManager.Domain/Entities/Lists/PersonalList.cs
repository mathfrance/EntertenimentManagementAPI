using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.SharedContext;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class PersonalList : Entity, IEqualityComparer<PersonalList>
    {
        public PersonalList(string title)
        {
            Title = title;
            Items = new();
        }

        public string Title { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public List<IItem> Items { get; private set; }

        public bool Equals(PersonalList x, PersonalList y)
        {
            return x.Title == y.Title && x.Category == y.Category;
        }

        public int GetHashCode([DisallowNull] PersonalList obj)
        {
            return HashCode.Combine(Id, Title, Category);
        }
    }
}