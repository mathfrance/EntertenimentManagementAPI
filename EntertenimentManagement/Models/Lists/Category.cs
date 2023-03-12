using EntertenimentManagement.SharedContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertenimentManagement.Models.Lists
{
    public class Category : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Catalog Catalog { get; set; }
        public List<PersonalList> Lists { get; set; }
    }
}
