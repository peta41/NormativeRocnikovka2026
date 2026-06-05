using Microsoft.AspNetCore.Mvc;
using Normative.Data;
using Normative.Models;
using Normative.Models.Home;
using Normative.Models.Screen;
using System.Diagnostics;


namespace Normative.Controllers
{
    public class HomeController(ILogger<HomeController> logger, SharedService sharedService, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly SharedService _sharedService = sharedService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task<IActionResult> Index()  //break 25/5
        {

            if (_sharedService.Logged() == false) //break 25/5
            {
                return RedirectToAction("login", "Account");
            }

            HomeModel model = new()
            {
                Navigation = new(),
                ToolBar = new(),
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
