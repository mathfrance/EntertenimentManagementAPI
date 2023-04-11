using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Users
{
    public class Role : Entity
    {

        public string Name { get; private set; } = string.Empty;
        public List<User> Users { get; private set; }
    }
}
