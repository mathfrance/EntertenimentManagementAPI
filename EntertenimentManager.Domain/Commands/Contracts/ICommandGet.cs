namespace EntertenimentManager.Domain.Commands.Contracts
{
    public interface ICommandGet : ICommand
    {
        public int UserId { get; set; }

        public bool IsRequestFromAdmin { get; set; }
    }
}
