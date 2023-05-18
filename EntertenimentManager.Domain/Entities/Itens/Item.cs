using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public abstract class Item : Entity
    {
        protected Item(string title, string genre, int releaseYear, string urlImage = "")
        {
            Title = title;
            Genre = genre;
            UrlImage = urlImage;
            ReleaseYear = releaseYear;
        }

        public string Title { get; private set; } = string.Empty;
        public string Genre { get; private set; } = string.Empty;
        public string UrlImage { get; private set; } = string.Empty;
        public int ReleaseYear { get; private set; } = 0;

        public PersonalList BelongsTo { get; private set; }


        protected void Update(string title, string genre, int releaseYear, string urlImage, PersonalList belongsTo)
        {
            Title = title;
            Genre = genre;
            UrlImage = urlImage;
            ReleaseYear = releaseYear;
            BelongsTo = belongsTo;
        }
    }
}
