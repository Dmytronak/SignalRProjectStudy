using System.ComponentModel.DataAnnotations;

namespace SignalRProject.ViewModels.AuthViewModel
{
    public class LoginAuthView
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
