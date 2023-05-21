using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Game : Entity, IItem
    {
        public Game(string title, string genre, int releaseYear, string developer = "", string urlImage = "")
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            Developer = developer;
            UrlImage = urlImage;
            Platforms = new();
        }

        #region Properties
        public string Developer { get; private set; } = string.Empty;

        public List<Platform> Platforms { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Genre { get; private set; } = string.Empty;

        public string UrlImage { get; private set; } = string.Empty;

        public int ReleaseYear { get; private set; } = 0;

        public PersonalList BelongsTo { get; private set; }
        #endregion

        public void Update(string title, string genre, int releaseYear, string developer, string urlImage, List<Platform> platforms, PersonalList belongsTo)
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            Developer = developer;
            UrlImage = urlImage;
            Platforms = platforms;
            BelongsTo = belongsTo;
        }
    }
}
