using Normative.Models.Config;
using X.PagedList;

namespace Normative.Models.Screen.Settings
{
    public class ModelViewRole
    {
        public ToolBar ModelToolBar { get; set; }
        public IPagedList<Role> Roles { get; set; }
        public Navigation Navigation { get; set; }
    }
}
