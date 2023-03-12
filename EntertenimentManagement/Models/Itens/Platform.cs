using EntertenimentManagement.SharedContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EntertenimentManagement.Models.Itens
{
    public class Platform : Base
    {
        public string Description { get; set; } = string.Empty;

        public List<Game> Games { get; set; }
    }
}
