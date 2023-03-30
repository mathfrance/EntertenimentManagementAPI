using EntertenimentManager.Domain.Commands.Contracts;

namespace EntertenimentManager.Domain.Handlers.Contract
{
    public interface IHandler<T> where T: ICommand
    {
        ICommandResult Handle(T command);
    }
}
