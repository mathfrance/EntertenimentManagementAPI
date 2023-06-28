using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public abstract class Item : Entity
    {
        public Item()
        {

        }
        protected Item(string title, string genre, int releaseYear, string urlImage, PersonalList belongsTo)
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            UrlImage = urlImage;
            BelongsTo = belongsTo;
        }

        public string Title { get; private set; } = string.Empty;
        public string Genre { get; private set; } = string.Empty;
        public string UrlImage { get; private set; } = string.Empty;
        public int ReleaseYear { get; private set; } = 0;
        public PersonalList BelongsTo { get; private set; }


        protected void Update(string title, string genre, int releaseYear, string urlImage)
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            if (!string.IsNullOrEmpty(urlImage))
                UrlImage = urlImage;
        }

        public void SwitchPersonalList(PersonalList personalList)
        {
            BelongsTo ??= personalList;
        }
    }
}
