using EntertenimentManager.Domain.Commands.Contracts;
using System.Threading.Tasks;

namespace EntertenimentManager.Domain.Handlers.Contract
{
    public interface IHandler<T> where T: ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
