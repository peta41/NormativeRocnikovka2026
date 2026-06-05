using System.ComponentModel.DataAnnotations;

namespace Normative.Models.Config;

public class UserCreateModel
{
    public int? UserId { get; set; }
    [Required]
    public string Username { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password),
        ErrorMessage = "Passwors do not match")]
    public string ConfirmPassword { get; set; }

    [Required]
    public int RoleId { get; set; }
}
