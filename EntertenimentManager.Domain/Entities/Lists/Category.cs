using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Category : Entity
    {
        public Category(string name, Catalog catalog, string description = "", List<PersonalList> lists = null)
        {
            Name = name;
            Catalog = catalog;
            Description = description;
            Lists = lists ?? (new());
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Catalog Catalog { get; set; }
        public List<PersonalList> Lists { get; set; }
    }
}
