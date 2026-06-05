using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Normative.Data;
using Normative.Models;
using Normative.Models.Screen;

namespace Normative.Controllers
{
    [Authorize]
    public class OutherController : Controller
    {
        private static NormativeContext _ctx { get; set; }
        public OutherController(NormativeContext ctx) 
        { 
                _ctx = ctx;
        }

        [Authorize]
        public IActionResult Index()
        {

            OutherVessel model = new();

            List<V_VTC_OutherVessel_OPSQ_10> op10 = _ctx.V_VTC_OutherVessel_OPSQ_10.ToList();
            List<V_VTC_OutherVessel_OPSQ_20> op20 = _ctx.V_VTC_OutherVessel_OPSQ_20.ToList();
            List<V_VTC_OutherVessel_OPSQ_40> op40 = _ctx.V_VTC_OutherVessel_OPSQ_40.ToList();

            model.Op10 = op10;
            model.Op20 = op20;
            model.Op40 = op40;

            //zac
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink {
                        Controller = "Home",
                        Action = "Index",
                        Title = "Home",
                        Icon = "icon-home"
                    },
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };

            model.ToolBar = tool;
            //kon

            return View(model);
        }
    }
}
