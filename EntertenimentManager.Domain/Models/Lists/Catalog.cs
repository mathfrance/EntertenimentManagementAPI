using EntertenimentManager.Domain.Models.Users;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Lists
{
    public class Catalog : Base
    {
        public string Description { get; set; } = string.Empty;
        public User Owner { get; set; }
        public List<Category> Categories { get; set; }
    }
}
