using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Catalog : Entity
    {
        public Catalog(string description, User owner, List<Category> categories = null)
        {
            Description = description;
            Owner = owner;
            Categories = categories ?? (new());
        }

        public string Description { get; set; } = string.Empty;
        public User Owner { get; set; }
        public List<Category> Categories { get; set; }
    }
}
