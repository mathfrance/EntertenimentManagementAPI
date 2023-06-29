using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Game : Item
    {
        private readonly List<Platform> _platforms;
        public Game()
        {
            
        }
        public Game(string title, string genre, int releaseYear, PersonalList belongsTo, string developer = "", string urlImage = "")
            : base(title, genre, releaseYear, urlImage, belongsTo)
        {
            Developer = developer;
            _platforms = new();
        }

        public string Developer { get; private set; } = string.Empty;

        public IReadOnlyCollection<Platform> Platforms { get { return _platforms.ToArray(); } }

        public void Update(string title, string genre, int releaseYear, string developer, string urlImage, List<Platform> platforms)
        {
            base.Update(title, genre, releaseYear, urlImage);
            Developer = developer;
            this.UpdatePlatforms(platforms);
        }

        private void UpdatePlatforms(List<Platform> platforms)
        {
            
        }
    }
}
