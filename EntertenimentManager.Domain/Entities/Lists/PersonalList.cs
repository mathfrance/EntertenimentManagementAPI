using System.Collections.Generic;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.SharedContext;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public class PersonalList : Entity
    {
        public PersonalList(string title, bool exclusive)
        {
            Title = title;
            Exclusive = exclusive;
            Movies = new(); 
            Games = new();
        }

        public string Title { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public bool Exclusive { get; private set; } = false;
        public List<Movie> Movies { get; private set; }
        public List<Game> Games { get; private set; }
    }
}
