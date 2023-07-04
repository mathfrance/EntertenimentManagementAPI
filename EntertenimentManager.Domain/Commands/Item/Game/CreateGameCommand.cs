using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Commands.Item.Game
{
    public class CreateGameCommand : Notifiable<Notification>, ICommandTokenAuthorization
    {
        private readonly int _maxFutureYearRelase;

        public CreateGameCommand()
        {
            _maxFutureYearRelase = DateTime.Now.AddYears(5).Year;
        }

        public CreateGameCommand(string developer, string title, string genre, int releaseYear, int belongsToId, Image thumbImage, List<int> platforms)
        {
            Developer = developer;
            Title = title;
            Genre = genre;
            ReleaseYear = releaseYear;
            BelongsToId = belongsToId;
            ThumbImage = thumbImage;
            Platforms = platforms;
            _maxFutureYearRelase = DateTime.Now.AddYears(5).Year;
        }

        public string Developer { get; set; } = string.Empty;
        public List<int> Platforms { get; set; } = new();
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
            .IsLowerThan(Developer, 80, "A desenvolvedora precisa ter no máximo 80 caracteres")
                .IsLowerThan(Genre, 50, "O gênero precisa ter no máximo 50 caracteres")
                .IsGreaterThan(ReleaseYear, 1900, "O jogo precisa ter sido lançado após o ano 1900")
                .IsLowerThan(ReleaseYear, _maxFutureYearRelase, $"O jogo precisa ter data de lançamento até {_maxFutureYearRelase}")
                );

            AddNotifications(ThumbImage.Notifications);
        }
    }
}
