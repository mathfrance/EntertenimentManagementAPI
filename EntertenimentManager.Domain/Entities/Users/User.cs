using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;
using System;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Entities.Categories.Contracts;
using EntertenimentManager.Domain.Entities.Categories;

namespace EntertenimentManager.Domain.Entities.Users
{
    public class User : Entity
    {
        private readonly List<Role> _roles;
        private readonly List<Category> _categories;
        private readonly ICategoryFactory _categoryFactory;
        
        public User(string name, string email, string passwordHash, string image, ICategoryFactory categoryFactory)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Image = image;
            _categoryFactory = categoryFactory;
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
                var personalLists = _categoryFactory.Create(category);
                _categories.Add(new Category(category.ToString(), (int)category, personalLists));
            }
        }

        public void UpdateCategories()
        {
            foreach (EnumCategories category in Enum.GetValues(typeof(EnumCategories)))
            {
                if (!_categories.Exists(x => x.Type == (int)category))
                {
                    var personalLists = _categoryFactory.Create(category);
                    _categories.Add(new Category(category.ToString(), (int)category, personalLists));
                }
                    
            }
        }
        #endregion
    }
}
