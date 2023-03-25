namespace EntertenimentManager.Domain.Models.Itens
{
    public class Movie : Item
    {
        public string Distributor { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; } = 0;

    }
}
