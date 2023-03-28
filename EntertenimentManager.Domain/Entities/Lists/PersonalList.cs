using System.Collections.Generic;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.SharedContext;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class PersonalList : Entity
    {
        public PersonalList(string title, Category category, bool exclusive, List<Movie> movies = null, List<Game> games = null)
        {
            Title = title;
            Category = category;
            Exclusive = exclusive;
            Movies = movies ?? (new()); 
            Games = games ?? (new());
        }

        public string Title { get; set; } = string.Empty;
        public Category Category { get; set; }
        public bool Exclusive { get; set; } = false;
        public List<Movie> Movies { get; set; }
        public List<Game> Games { get; set; }
    }
}
