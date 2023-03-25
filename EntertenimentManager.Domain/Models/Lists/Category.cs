using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Lists
{
    public class Category : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Catalog Catalog { get; set; }
        public List<PersonalList> Lists { get; set; }
    }
}
