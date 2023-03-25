using System.Collections.Generic;
using EntertenimentManager.Domain.Models.Itens;
using EntertenimentManager.Domain.SharedContext;

namespace EntertenimentManager.Domain.Models.Lists
{
    public class PersonalList : Base
    {
        public string Title { get; set; } = string.Empty;
        public Category Category { get; set; }
        public bool Exclusive { get; set; } = false;
        public List<Movie> Movies { get; set; }
        public List<Game> Games { get; set; }
    }
}
