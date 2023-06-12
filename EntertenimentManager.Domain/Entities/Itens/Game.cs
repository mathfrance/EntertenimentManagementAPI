using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Game : Item
    {
        public Game()
        {
            
        }
        public Game(string title, string genre, int releaseYear, PersonalList belongsTo, string developer = "", string urlImage = "")
            : base(title, genre, releaseYear, urlImage, belongsTo)
        {
            Developer = developer;
            Platforms = new();
        }

        public string Developer { get; private set; } = string.Empty;

        public List<Platform> Platforms { get; private set; }

        public void Update(string title, string genre, int releaseYear, string developer, string urlImage, List<Platform> platforms, PersonalList belongsTo)
        {
            base.Update(title, genre, releaseYear, urlImage, belongsTo);
            Developer = developer;
            Platforms = platforms;
        }
    }
}
