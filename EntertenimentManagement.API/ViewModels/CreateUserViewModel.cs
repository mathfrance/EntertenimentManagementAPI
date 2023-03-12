using EntertenimentManagement.Models.Lists;

namespace EntertenimentManagement.API.ViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
        public Catalog Catalog { get; set; }
    }
}
