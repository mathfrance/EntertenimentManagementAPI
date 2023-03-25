using EntertenimentManager.Domain.Models.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Users
{
    public class User : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Catalog Catalog { get; set; }
        public List<Role> Roles { get; set; }
    }
}
