using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Users
{
    public class Role : Base
    {
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; }
    }
}
