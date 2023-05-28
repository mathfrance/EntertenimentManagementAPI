﻿using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using Flunt.Notifications;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Users;

namespace EntertenimentManager.Domain.Handlers
{
    public class MovieHandler :
        Notifiable<Notification>,
        IHandler<CreateMovieCommand>
    {
        private readonly IMovieRepository _repository;
        private readonly IImageStorage _imageStorage;

        public MovieHandler(IMovieRepository repository, IImageStorage storage)
        {
            _repository = repository;
            _imageStorage = storage;
        }
        public async Task<ICommandResult> Handle(CreateMovieCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível criar o filme", command.Notifications);

            var personalList = await _repository.GetPersonalListById(command.BelongsToId);

            if (personalList == null)
                return new GenericCommandResult(false, "Não foi possível adicionar o filme à lista", command.Notifications);

            var movie = new Movie(
                command.Title, 
                command.Genre, 
                command.ReleaseYear, 
                command.DurationInMinutes, 
                command.ThumbImage.FileName, 
                command.Distributor, 
                command.Director);

            await _repository.CreateAsync(movie);

            await _imageStorage.UploadAsync(command.ThumbImage.ImageBytes, command.ThumbImage.FileName);

            return new GenericCommandResult(true, "Filme criado com sucesso", movie);
        }
    }
}
