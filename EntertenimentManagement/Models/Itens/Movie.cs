using EntertenimentManagement.Models.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EntertenimentManagement.Models.Itens
{
    public class Movie : Item
    {
        public string Distributor { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; } = 0;
        
    }
}
