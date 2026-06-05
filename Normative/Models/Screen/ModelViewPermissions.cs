using Normative.Models.View;

namespace Normative.Models.Screen.Settings
{
    public class ModelViewPermissions
    {
        public ToolBar Toolbar { get; set; }
        public List<ViewPermission> Permissions { get; set; }
        public Navigation Navigation { get; set; }
    }
}
