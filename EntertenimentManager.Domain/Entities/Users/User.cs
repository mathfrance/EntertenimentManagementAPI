using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Users
{
    public class User : Entity
    {
        private List<Role> _roles;
        public User(string name, string email, string passwordHash, string image = "")
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Image = image;
            _roles = new();
        }
        #region Properties
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Image { get; private set; } = string.Empty;
        public Catalog Catalog { get; private set; }
        public IReadOnlyCollection<Role> Roles { get { return _roles.ToArray(); } }
        #endregion

        #region CRUD Properties
        public void UpdateImage(string image) => Image = image;

        public void AddRole(Role role)
        {
            if (role != null) _roles.Add(role);
        }

        public void AddRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                this.AddRole(role);
            }
        }
        #endregion
    }
}
