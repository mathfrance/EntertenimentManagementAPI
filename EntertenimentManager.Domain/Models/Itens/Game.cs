using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Itens
{
    public class Game : Item
    {
        public string Developer { get; set; } = string.Empty;

        public List<Platform> Platforms { get; set; }
    }
}
