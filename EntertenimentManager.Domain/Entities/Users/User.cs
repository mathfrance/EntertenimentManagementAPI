using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System;
using EntertenimentManager.Domain.Enumerators;

namespace EntertenimentManager.Domain.Entities.Users
{
    public class User : Entity
    {
        private readonly List<Role> _roles;
        private readonly List<Category> _categories;
        public User(string name, string email, string passwordHash, string image)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Image = image;
            _roles = new();
            _categories = new();
        }
        #region Properties
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Image { get; private set; } = string.Empty;
        public IReadOnlyCollection<Category> Categories { get { return _categories.ToArray(); } }
        public IReadOnlyCollection<Role> Roles { get { return _roles.ToArray(); } }
        #endregion

        #region CRUD Properties
        public void Update(string name, string email, string passwordHash, string image = "")
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            if (!string.IsNullOrEmpty(image))
                Image = image;
        }

        public void AddRole(Role role)
        {
            if (role != null && !_roles.Exists(x => x.Id == role.Id)) _roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            if (role != null && _roles.Exists(x => x.Id == role.Id)) _roles.RemoveAll(x => x.Id == role.Id);
        }

        public void CreateCategories()
        {
            foreach (EnumCategories category in Enum.GetValues(typeof(EnumCategories)))
            {
                CategoryFactory factory = new CategoryFactory();
                var result = factory.Create(category);
                _categories.Add(new Category(category.ToString(), (int)category, result));
            }
        }

        public void UpdateCategories()
        {
            foreach (EnumCategories category in Enum.GetValues(typeof(EnumCategories)))
            {
                //if (!_categories.Exists(x => x.Type == (int)category))
                //_categories.Add(new Category(category.ToString(), (int)category));
            }
        }
        #endregion
    }
}
