using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class Category : Entity
    {
        public Category()
        {
            
        }
        public Category(string name, int type, IEnumerable<IPersonalList> list)
        {
            Name = name;
            Type = type;
            Lists = list.ToList();
        }

        public string Name { get; private set; } = string.Empty;
        public int Type { get; private set; }
        public User Owner { get; private set; }
        public IList<IPersonalList> Lists { get; private set; }
    }
}
