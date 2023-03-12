using EntertenimentManagement.SharedContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertenimentManagement.Models.Users
{
    public class Role : Base
    {
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; }
    }
}
