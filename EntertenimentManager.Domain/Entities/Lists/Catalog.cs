using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Catalog : Entity
    {
        public Catalog(string description)
        {
            Description = description;
            Categories = new();
        }

        public string Description { get; private set; } = string.Empty;
        public User Owner { get; private set; }
        public List<Category> Categories { get; private set; }
    }
}
