using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Users
{
    public class User : Entity
    {
        public User(string name, string email, string passwordHash, List<Role> roles, string image = "")
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Image = image;
            Roles = roles;
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Catalog Catalog { get; set; }
        public List<Role> Roles { get; set; }
    }
}
