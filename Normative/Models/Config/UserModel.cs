using Normative.Models.Screen;

namespace Normative.Models.Config
{
    public class UserModel
    {
        public List<Role> Roles { get; set; }
        public UserCreateModel Request { get; set; }
        public UserEditModel Edit { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}