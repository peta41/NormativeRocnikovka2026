using Normative.Models.Config;
using X.PagedList;

namespace Normative.Models.Screen.Settings
{
    public class ModelViewUser
    {
        public ToolBar ModelToolBar { get; set; }
        public IPagedList<ViewUsersRoles> Users { get; set; }
        public Navigation Navigation { get; set; }

        //public List<Role> Roles { get; set; }
    }
}
