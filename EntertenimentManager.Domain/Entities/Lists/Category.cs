using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Category : Entity
    {
        public Category(string name, int type)
        {
            Name = name;
            Type = type;
            Lists = new();
        }

        public string Name { get; private set; } = string.Empty;
        public int Type { get; private set; }
        public User Owner { get; private set; }
        public List<PersonalList> Lists { get; private set; }
    }
}
