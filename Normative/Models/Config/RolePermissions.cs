using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Normative.Models.Config
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Permissions Permission { get; set; }
    }
}
