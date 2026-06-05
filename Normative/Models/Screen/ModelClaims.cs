using System.Security.Claims;

namespace Normative.Models.Screen.Settings
{
    public class ModelClaims
    {
        //public var ClaimsRow { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public ToolBar ToolBar { get; set; }
        public Navigation Navigation { get; set; }
    }
}
