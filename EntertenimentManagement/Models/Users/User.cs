using EntertenimentManagement.Models.Lists;
using EntertenimentManagement.SharedContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertenimentManagement.Models.Users
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
