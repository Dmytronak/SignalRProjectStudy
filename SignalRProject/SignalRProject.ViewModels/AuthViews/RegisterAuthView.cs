using System.ComponentModel.DataAnnotations;

namespace SignalRProject.ViewModels.AuthViews
{
    public class RegisterAuthView
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Pasword is required")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords is not confirmed")]
        public string ConfirmPassword { get; set; }
    }
}
