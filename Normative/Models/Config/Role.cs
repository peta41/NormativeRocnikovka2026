using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Normative.Models.Config
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserRole> UsersWithRole { get; set; }
    }
}
