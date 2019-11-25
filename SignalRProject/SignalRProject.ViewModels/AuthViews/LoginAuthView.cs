using System.ComponentModel.DataAnnotations;

namespace SignalRProject.ViewModels.AuthViews
{
    public class LoginAuthView
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
