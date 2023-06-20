using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace EntertenimentManager.Domain.Commands.Item.Movie
{
    public class CreateMovieCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        private readonly int _maxFutureYearRelase;
        public CreateMovieCommand()
        {
            _maxFutureYearRelase = DateTime.Now.AddYears(5).Year;
        }

        public CreateMovieCommand(string distributor, string director, int durationInMinutes, string title, string genre, string urlImage, int releaseYear, int belongsToId)
        {
            Distributor = distributor;
            Director = director;
            DurationInMinutes = durationInMinutes;
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            BelongsToId = belongsToId;
            ThumbImage = new(urlImage);
            _maxFutureYearRelase = DateTime.Now.AddYears(5).Year;
        }

        public string Distributor { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; } = 0;
        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public Image ThumbImage { get; set; }

        public int ReleaseYear { get; set; } = 0;

        public int BelongsToId { get; set; }
        public int UserId { get; set; } = 0;
        public bool IsRequestFromAdmin { get; set; } = false;

        public void Validate()
        {
            AddNotifications(new Contract<CreateAccountCommand>()
                .Requires()
                .IsNotNullOrEmpty(Title, "Informe o título")
                .IsLowerThan(Title, 80, "O título precisa ter no máximo 80 caracteres")
                .IsLowerThan(Distributor, 80, "A Distribuidora precisa ter no máximo 80 caracteres")
                .IsLowerThan(Director, 80, "O(A) Diretor(a) precisa ter no máximo 80 caracteres")
                .IsLowerThan(Genre, 50, "O gênero precisa ter no máximo 50 caracteres")
                .IsGreaterThan(ReleaseYear, 1900, "O filme precisa ter sido lançado após o ano 1900")
                .IsLowerThan(ReleaseYear, _maxFutureYearRelase, $"O filme precisa ter data de lançamento até {_maxFutureYearRelase}")
                .IsGreaterThan(DurationInMinutes, 1, "O filme precisa ter duração mínima de 1 minuto")
                .IsLowerThan(DurationInMinutes, 999, "O filme precisa ter duração máxima de 999 minutos")
                );
             AddNotifications(ThumbImage.Notifications);
        }
    }
}
