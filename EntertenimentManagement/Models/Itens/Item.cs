using EntertenimentManagement.Models.Lists;
using EntertenimentManagement.SharedContext;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EntertenimentManagement.Models.Itens
{
    public abstract class Item : Base
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;   
        public string UrlImage { get; set; } = string.Empty;
        public int ReleaseYear { get; set; } = 0;

        public List<PersonalList> BelongsTo { get; set; }

    }
}
