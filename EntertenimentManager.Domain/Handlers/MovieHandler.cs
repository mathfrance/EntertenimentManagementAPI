using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Entities.Itens;

namespace EntertenimentManager.Domain.Handlers
{
    public class MovieHandler :
        Notifiable<Notification>,
        IHandler<CreateMovieCommand>,
        IHandler<UpdateMovieCommand>,
        IHandler<GetMovieByIdCommand>
        
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
                command.Distributor,
                command.Director,
                personalList,
                command.DurationInMinutes, 
                command.ThumbImage.FileName
                );
            await _repository.CreateAsync(movie);

            await _imageStorage.UploadAsync(command.ThumbImage.ImageBytes, command.ThumbImage.FileName);

            return new GenericCommandResult(true, "Filme criado com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(UpdateMovieCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível atualizar as informações do filme", command.Notifications);

            var movie = await _repository.GetById(command.Id);

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

            await _repository.UpdateAsync(movie);

            return new GenericCommandResult(true, "Filme atualizado com sucesso", movie);
        }

        public async Task<ICommandResult> Handle(GetMovieByIdCommand command)
        {
            if(!command.IsRequestFromAdmin && !await _repository.IsMovieAssociatedWithUserIdAsync(command.Id, command.UserId))
            {
                return new GenericCommandResult(false, "Não foi possível obter o filme", command.Notifications);
            }
            var movie = await _repository.GetById(command.Id);

            if (movie == null)
                return new GenericCommandResult(false, "Não foi possível obter o filme", command.Notifications);

            return new GenericCommandResult(true, "Filme obtida com sucesso", movie);
        }
    }
}
