﻿using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Commands.Item;
using EntertenimentManager.Domain.Enumerators;

namespace EntertenimentManager.Domain.Handlers
{
    public class MovieHandler :
        Notifiable<Notification>,
        IHandler<CreateMovieCommand>,
        IHandler<UpdateMovieCommand>,
        IHandler<GetMovieByIdCommand>,
        IHandler<DeleteMovieCommand>,
        IHandler<GetAllByPersonalListIdCommand>,
        IHandler<SwitchPersonalListFromItemCommand>

    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPersonalListRepository _personalListRepository;
        private readonly IImageStorage _imageStorage;

        public MovieHandler(
            IMovieRepository movieRepository, 
            IPersonalListRepository personalListRepository,
            IImageStorage storage)
        {
            _movieRepository = movieRepository;
            _personalListRepository = personalListRepository;
            _imageStorage = storage;
        }
        public async Task<ICommandResult> Handle(CreateMovieCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível criar o filme", command.Notifications);

            if (!command.IsRequestFromAdmin && !await _personalListRepository.IsPersonalListAssociatedWithUserIdAsync(command.BelongsToId, command.UserId))
                return new GenericCommandResult(false, "Não é possível criar o filme na lista informada", command.Notifications);

            var personalList = await _movieRepository.GetPersonalListById(command.BelongsToId);

            if (personalList == null)
                return new GenericCommandResult(false, "Não foi possível adicionar o filme à lista", command.Notifications);

            if (personalList.Category == null || personalList.Category.Type != (int)EnumCategories.Movies)
                return new GenericCommandResult(false, "Lista informada não é da categoria de filmes", command.Notifications);

            var movie = new Movie(
                command.Title, 
                command.Genre, 
                command.ReleaseYear, 
                command.Distributor,
                command.Director,
                personalList,
                command.DurationInMinutes, 
                command.ThumbImage.FileName
                );
            await _movieRepository.CreateAsync(movie);

            await _imageStorage.UploadAsync(command.ThumbImage.ImageBytes, command.ThumbImage.FileName);

            return new GenericCommandResult(true, "Filme criado com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(UpdateMovieCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível atualizar as informações do filme", command.Notifications);

            if (!command.IsRequestFromAdmin && !await _movieRepository.IsItemAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Filme indisponível", command.Notifications);

            var movie = await _movieRepository.GetById(command.Id);

            if(movie == null) 
                return new GenericCommandResult(false, "Não foi possível atualizar as informações do filme", command.Notifications);

            if (command.HasImageToUpdate())
            {
                movie.Update(command.Title, command.Genre, command.ReleaseYear, command.DurationInMinutes, command.Distributor, command.Director, command.NewImage.FileName);
                await _imageStorage.UploadAsync(command.NewImage.ImageBytes, command.NewImage.FileName);
            }
            else
            {
                movie.Update(command.Title, command.Genre, command.ReleaseYear, command.DurationInMinutes, command.Distributor, command.Director, string.Empty);
            }

            await _movieRepository.UpdateAsync(movie);

            return new GenericCommandResult(true, "Filme atualizado com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(GetMovieByIdCommand command)
        {
            if(!command.IsRequestFromAdmin && !await _movieRepository.IsItemAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Filme indisponível", command.Notifications);

            var movie = await _movieRepository.GetById(command.Id);

            if (movie == null)
                return new GenericCommandResult(false, "Não foi possível obter o filme", command.Notifications);

            return new GenericCommandResult(true, "Filme obtida com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(DeleteMovieCommand command)
        {
            if (!command.IsRequestFromAdmin && !await _movieRepository.IsItemAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Filme indisponível", command.Notifications);

            var movie = await _movieRepository.GetById(command.Id);

            if (movie == null)
                return new GenericCommandResult(false, "Não foi possível realizar a exclusão do filme", command.Notifications);

            await _movieRepository.DeleteAsync(movie);

            return new GenericCommandResult(true, "Filme excluído com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(GetAllByPersonalListIdCommand command)
        {
            if (!command.IsRequestFromAdmin && !await _personalListRepository.IsPersonalListAssociatedWithUserIdAsync(command.PersonalListId, command.UserId))
                return new GenericCommandResult(false, "Lista indisponível", command.Notifications);

            var personalList = await _movieRepository.GetPersonalListById(command.PersonalListId);

            if (personalList == null || personalList.Category == null || personalList.Category.Type != (int)EnumCategories.Movies)
                return new GenericCommandResult(false, "Lista informada não é da categoria de filmes", command.Notifications);

            var movies = await _movieRepository.GetAllByPersonalId(command.PersonalListId);

            return new GenericCommandResult(true, "Filmes obtidos com sucesso", movies);
        }

        public async Task<ICommandResult> Handle(SwitchPersonalListFromItemCommand command)
        {
            if (!command.IsRequestFromAdmin)
            {
                if (!await _movieRepository.IsItemAssociatedWithUserIdAsync(command.ItemId, command.UserId))
                    return new GenericCommandResult(false, "Filme indisponível para troca", command.Notifications);
                if (!await _personalListRepository.IsPersonalListAssociatedWithUserIdAsync(command.NewPersonalListId, command.UserId))
                    return new GenericCommandResult(false, "Lista indisponível para troca", command.Notifications);
            }

            var personalList = await _movieRepository.GetPersonalListById(command.NewPersonalListId);

            if (personalList == null)
                return new GenericCommandResult(false, "Lista não encontrada", command.Notifications);

            if (personalList.Category == null || !await _movieRepository.IsSwitchBetweenSameTypePersonalLists(command.ItemId, personalList.Category.Type))
                return new GenericCommandResult(false, "Não é possível realizar a troca para a lista informada", command.Notifications);

            var movie = await _movieRepository.GetById(command.ItemId);

            if (movie == null)
                return new GenericCommandResult(false, "Filme não encontrado", command.Notifications);

            movie.SwitchPersonalList(personalList);

            await _movieRepository.UpdateAsync(movie);

            return new GenericCommandResult(true, "Troca realizada com sucesso", null);
        }

    }
}
