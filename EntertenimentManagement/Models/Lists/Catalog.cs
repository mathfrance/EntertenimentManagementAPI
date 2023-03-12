using EntertenimentManagement.Models.Users;
using EntertenimentManagement.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManagement.Models.Lists
{
    public class Catalog : Base
    {
        public string Description { get; set; } = string.Empty;
        public User Owner { get; set; }
        public List<Category> Categories { get; set; }
    }
}
