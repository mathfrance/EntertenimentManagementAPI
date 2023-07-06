using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Contracts;
using EntertenimentManager.Domain.Commands.Item.Game;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Handlers.Contract;
using EntertenimentManager.Domain.Repositories.Contracts;
using Flunt.Notifications;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Handlers
{
    public class GameHandler :
        Notifiable<Notification>,
        IHandler<CreateGameCommand>,
        IHandler<UpdateGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPersonalListRepository _personalListRepository;
        private readonly IImageStorage _imageStorage;

        public GameHandler(
            IGameRepository gameRepository,
            IPersonalListRepository personalListRepository,
            IImageStorage storage)
        {
            _gameRepository = gameRepository;
            _personalListRepository = personalListRepository;
            _imageStorage = storage;
        }
        public async Task<ICommandResult> Handle(CreateGameCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível criar o jogo", command.Notifications);

            if (!command.IsRequestFromAdmin && !await _personalListRepository.IsPersonalListAssociatedWithUserIdAsync(command.BelongsToId, command.UserId))
                return new GenericCommandResult(false, "Não é possível criar o jogo na lista informada", command.Notifications);

            var personalList = await _gameRepository.GetPersonalListById(command.BelongsToId);

            if (personalList == null)
                return new GenericCommandResult(false, "Não foi possível adicionar o jogo à lista", command.Notifications);

            if (personalList.Category == null || personalList.Category.Type != (int)EnumCategories.Games)
                return new GenericCommandResult(false, "Lista informada não é da categoria de jogos", command.Notifications);

            var platforms = await _gameRepository.GetPlatformsByIds(command.Platforms);

            var game = new Game(
                command.Title,
                command.Genre,
                command.ReleaseYear,
                personalList,
                command.Developer,
                command.ThumbImage.FileName, 
                platforms
                );
            await _gameRepository.CreateAsync(game);

            await _imageStorage.UploadAsync(command.ThumbImage.ImageBytes, command.ThumbImage.FileName);

            return new GenericCommandResult(true, "Jogo criado com sucesso", game);
        }

        public async Task<ICommandResult> Handle(UpdateGameCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Não foi possível atualizar as informações do jogo", command.Notifications);

            if (!command.IsRequestFromAdmin && !await _gameRepository.IsItemAssociatedWithUserIdAsync(command.Id, command.UserId))
                return new GenericCommandResult(false, "Jogo indisponível", command.Notifications);

            var game = await _gameRepository.GetById(command.Id);

            if (game == null)
                return new GenericCommandResult(false, "Não foi possível atualizar as informações do jogo", command.Notifications);

            var platforms = await _gameRepository.GetPlatformsByIds(command.Platforms);

            if (command.HasImageToUpdate())
            {
                game.Update(command.Title, command.Genre, command.ReleaseYear, command.Developer, command.NewImage.FileName, platforms);
                await _imageStorage.UploadAsync(command.NewImage.ImageBytes, command.NewImage.FileName);
            }
            else
            {
                game.Update(command.Title, command.Genre, command.ReleaseYear, command.Developer, string.Empty, platforms);
            }

            await _gameRepository.UpdateAsync(game);

            return new GenericCommandResult(true, "Jogo atualizado com sucesso", game);
        }
    }
}
