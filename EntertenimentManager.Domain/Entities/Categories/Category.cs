using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Categories
{
    public class Category : Entity
    {
        public Category()
        {

        }
        public Category(string name, int type, IEnumerable<PersonalList> list)
        {
            Name = name;
            Type = type;
            Lists = list.ToList();
        }

        public string Name { get; private set; } = string.Empty;
        public int Type { get; private set; }
        public User Owner { get; private set; }
        public IList<PersonalList> Lists { get; private set; }
    }
}
