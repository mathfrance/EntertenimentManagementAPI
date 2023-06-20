namespace EntertenimentManager.Domain.Commands.Contracts
{
    public interface ICommandTokenAuthorization : ICommand
    {
        public int UserId { get; set; }

        public bool IsRequestFromAdmin { get; set; }
    }
}
