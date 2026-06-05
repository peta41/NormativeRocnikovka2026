using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Normative.Data;
using Normative.Models;
using Normative.Models.Screen;
using Normative.Models.STD.Pipe;

namespace Normative.Controllers
{
    [Authorize]
    public class PipeController : Controller
    {
        private static NormativeContext _ctx { get; set; }
        public PipeController(NormativeContext ctx)
        {
            _ctx = ctx;
        }

        //[Authorize]
        public IActionResult Index()
        {

            //PipeOperation model = new();

            //List<V_VTC_Pipe_OPSQ_20> op20 = _ctx.V_VTC_PipeOperation20.ToList();
            //List<V_VTC_Pipe_OPSQ_30> op30 = _ctx.V_VTC_PipeOperation30.ToList();

            List<V_VTC_Pipe_20_30> pipe = _ctx.V_VTC_PipeOperation_20_30.ToList();
            List<PreparationType> type = _ctx.PreparationType.ToList();

            PipeModel model  = new() { V_VTC_Pipe_20_30 = pipe, PreparationTypes = type };

            //model.Op20 = op20;
            //model.Op30 = op30;

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


            return View(model);
        }
    }
}