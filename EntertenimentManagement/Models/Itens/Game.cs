using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EntertenimentManagement.Models.Itens
{
    public class Game : Item
    {
        public string Developer { get; set; } = string.Empty;

        public List<Platform> Platforms { get; set; }       
    }
}
