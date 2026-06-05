using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Normative.Models;

public class LoginModel
{
    //temporaryUserAccount
    [Required(ErrorMessage = "Username or Email is required")]
    [MaxLength(320, ErrorMessage = "Max 20 characters")]
    [DisplayName("Username or Email")]

    public string UsernameOrEmail { get; set; }
    [Required(ErrorMessage = "Pssword is required")]
    [StringLength(320, MinimumLength = 8, ErrorMessage = "Max 50 or min 5 characters allowed")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
