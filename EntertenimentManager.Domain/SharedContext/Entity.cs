namespace EntertenimentManager.Domain.SharedContext
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = 0;
        }

        public int Id { get; private set; } = 0;
    }
}
