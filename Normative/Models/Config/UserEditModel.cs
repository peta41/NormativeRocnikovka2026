using System.ComponentModel.DataAnnotations;

namespace Normative.Models.Config
{
    public class UserEditModel
    {
        public int? UserId { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
