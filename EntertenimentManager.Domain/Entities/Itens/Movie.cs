using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Movie : Entity, IItem
    {
        public Movie(string title, string genre, int releaseYear, int durationInMinutes = 0, string distributor = "", string director = "", string urlImage = "")            
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            DurationInMinutes = durationInMinutes;
            Distributor = distributor;
            Director = director;
            UrlImage = urlImage;
        }
        #region Properties
        public string Distributor { get; private set; } = string.Empty;
        public string Director { get; private set; } = string.Empty;
        public int DurationInMinutes { get; private set; } = 0;

        public string Title { get; private set; } = string.Empty;

        public string Genre { get; private set; } = string.Empty;

        public string UrlImage { get; private set; } = string.Empty;

        public int ReleaseYear { get; private set; } = 0;

        public PersonalList BelongsTo { get; private set; }
        #endregion

        public void Update(string title, string genre, int releaseYear, int durationInMinutes, string distributor, string director, string urlImage, PersonalList belongsTo)
        {
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            DurationInMinutes = durationInMinutes;
            Distributor = distributor;
            Director = director;
            UrlImage = urlImage;
            BelongsTo = belongsTo;
        }
    }
}
