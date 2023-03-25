using EntertenimentManager.Domain.Models.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Models.Itens
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
