namespace Normative.Models.Screen.Settings
{
    public class ViewUsersRoles
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
    }
}
