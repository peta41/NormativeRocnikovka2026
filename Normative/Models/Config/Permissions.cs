using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Normative.Models.Config
{
    public partial class Permissions
    {
        public int PermissionId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public virtual ICollection<RolePermission> Roles { get; set; }
    }
    public enum EnumPermmisions
    {
        ViewForm = 0,
        New = 10,
        Purchase = 20,
        ENG = 30,
        Technology = 40,
        Other = 50,
        //60-79
        QC = 80,
        QCAdmin = 85,
        Admin = 90,
    }
    public class FieldList
    {
        public string Name { get; set; }
        public bool Writable { get; set; }
    }
    public class FieldPermmition
    {
        public EnumPermmisions Name { get; set; }
        public bool Writable { get; set; }
    }

}
