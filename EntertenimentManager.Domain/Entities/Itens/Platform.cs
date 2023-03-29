using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Platform : Entity
    {
        public Platform(string description)
        {
            Description = description;
            Games = new();
        }

        public string Description { get; private set; } = string.Empty;

        public List<Game> Games { get; private set; }
    }
}
