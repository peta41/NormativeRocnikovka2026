using System.ComponentModel.DataAnnotations;

namespace Normative.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "DisplayName is required")]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(320, ErrorMessage = "Max 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Pssword is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Max 50 or min 5 characters allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
