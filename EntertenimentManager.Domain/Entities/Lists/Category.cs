using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Category : Entity
    {
        public Category(string name,string description = "")
        {
            Name = name;
            Description = description;
            Lists = new();
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Catalog Catalog { get; private set; }
        public List<PersonalList> Lists { get; private set; }
    }
}
