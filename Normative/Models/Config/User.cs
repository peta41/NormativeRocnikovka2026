namespace Normative.Models.Config
{
        public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        
        public DateTime Created { get; set; }
        public bool IsActive { get; set; } = true;


        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
