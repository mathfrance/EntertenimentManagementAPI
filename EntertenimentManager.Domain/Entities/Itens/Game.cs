using EntertenimentManager.Domain.Entities.Lists;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Game : Item
    {
        private readonly List<Platform> _platforms;
        public Game()
        {
            _platforms = new();
        }
        public Game(string title, string genre, int releaseYear, PersonalList belongsTo, string developer, string urlImage, IEnumerable<Platform> platforms)
            : base(title, genre, releaseYear, urlImage, belongsTo)
        {
            Developer = developer;
            _platforms = platforms.ToList();
        }

        public string Developer { get; private set; } = string.Empty;

        public IReadOnlyCollection<Platform> Platforms { get { return _platforms.ToArray(); } }

        public void Update(string title, string genre, int releaseYear, string developer, string urlImage, IEnumerable<Platform> platforms)
        {
            base.Update(title, genre, releaseYear, urlImage);
            Developer = developer;
            this.UpdatePlatforms(platforms);
        }

        private void UpdatePlatforms(IEnumerable<Platform> platforms)
        {
            if (platforms != null)
            {
                _platforms.Clear();
                _platforms.AddRange(platforms);
            }
        }
    }
}
