using EntertenimentManager.Domain.Entities.Lists;

namespace EntertenimentManager.API.ViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
    }
}
