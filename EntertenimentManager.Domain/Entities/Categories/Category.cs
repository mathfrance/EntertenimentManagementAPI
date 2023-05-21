using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Categories
{
    public class Category : Entity
    {
        private readonly IList<PersonalList> _personalLists;
        protected Category()
        {
        }
        public Category(string name, int type, IEnumerable<PersonalList> list)
        {
            Name = name;
            Type = type;
            _personalLists = list.ToList();
        }

        public string Name { get; private set; } = string.Empty;
        public int Type { get; private set; }
        public User Owner { get; private set; }
        public IReadOnlyCollection<PersonalList> Lists { get { return _personalLists.ToArray(); } }
    }
}
