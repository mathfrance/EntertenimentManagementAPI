using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Itens
{
    public class Platform : Base
    {
        public string Description { get; set; } = string.Empty;

        public List<Game> Games { get; set; }
    }
}
