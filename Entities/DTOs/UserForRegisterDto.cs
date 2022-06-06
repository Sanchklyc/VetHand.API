using Core.Entities;

namespace Entities.DTOs
{
    public class UserForRegisterDto : IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gsm { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
    }
}
