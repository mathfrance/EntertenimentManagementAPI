using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Lists;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public class Movie : Item
    {
        public Movie()
        {
            
        }
        public Movie(string title, string genre, int releaseYear, string distributor, string director, PersonalList belongsTo, int durationInMinutes = 0, string urlImage = "")
            : base(title, genre, releaseYear, urlImage, belongsTo)
        {
            Distributor = distributor;
            Director = director;
            DurationInMinutes = durationInMinutes;
        }

        public string Distributor { get; private set; } = string.Empty;
        public string Director { get; private set; } = string.Empty;
        public int DurationInMinutes { get; private set; } = 0;

        public void Update(string title, string genre, int releaseYear, int durationInMinutes, string distributor, string director, string urlImage)
        {
            base.Update(title, genre, releaseYear, urlImage);
            Distributor = distributor;
            Director = director;
            DurationInMinutes = durationInMinutes;
        }

    }
}
