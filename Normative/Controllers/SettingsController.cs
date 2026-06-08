using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Config;
using Normative.Models.Screen;
using Normative.Models.Screen.Settings;
using Normative.Services;
using System.Security.Claims;
using X.PagedList;
using X.PagedList.Extensions;

namespace Normative.Controllers
{//prihlaseni + role Admin
    public class SettingsController(NormativeContext context, ILogger<SettingsController> logger, SharedService sharedService, LoginServices loginServices) : Controller
    {
        private readonly NormativeContext _ctx = context;
        private readonly ILogger<SettingsController> _logger = logger;
        private readonly SharedService _sharedService = sharedService;
        private readonly LoginServices _loginServices = loginServices;

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            ToolBar model = new ToolBar()
            {
                Action = new List<ToolBarActionLink> {
                    new ToolBarActionLink {
                        Controller = "Home",
                        Action = "Index",
                        Title = "Home",
                        Icon = "icon-home"
                    }
                }
            };


            return View(model);
        }



        [Authorize(Roles = "Administrator")]
        public async Task<ViewResult> Users(string search, string sortby, string order, int? page)
        {

            int pageSize = 50;
            if (page != null && page < 1)
            {
                page = 1;
            }

            //List<User> list = await _ctx.Users.OrderBy(s => s.DisplayName).ToListAsync();
            List<ViewUsersRoles> vur = await _ctx.ViewUsersRoles.ToListAsync();

            // Search
            if (search != null)
            {
                vur = vur.Where(w => w.Email.Contains(search) || w.DisplayName.Contains(search)).ToList();
            }
            ViewData["Search"] = search;

            // Order by
            switch (sortby)
            {
                case "Name":
                    if (order == null)
                    {
                        vur = vur.OrderBy(s => s.DisplayName).ToList();
                        ViewData["NameOrder"] = "desc";
                    }
                    else
                    {
                        vur = vur.OrderByDescending(s => s.DisplayName).ToList();
                        ViewData["NameOrder"] = "";
                    }
                    break;
                case "Email":
                    if (order == null)
                    {
                        vur = vur.OrderBy(s => s.Email).ToList();
                        ViewData["EmailOrder"] = "desc";
                    }
                    else
                    {
                        vur = vur.OrderByDescending(s => s.Email).ToList();
                        ViewData["EmailOrder"] = "";
                    }
                    break;
                case "UserName":
                    if (order == null)
                    {
                        vur = vur.OrderBy(s => s.UserName).ToList();
                        ViewData["UserNameOrder"] = "desc";
                    }
                    else
                    {
                        vur = vur.OrderByDescending(s => s.UserName).ToList();
                        ViewData["UserNameOrder"] = "";
                    }
                    break;
                case "Role":
                    if (order == null)
                    {
                        vur = vur.OrderBy(s => s.Role).ToList();
                        ViewData["RoleOrder"] = "desc";
                    }
                    else
                    {
                        vur = vur.OrderByDescending(s => s.UserName).ToList();
                        ViewData["RoleOrder"] = "";
                    }
                    break;
            }

            // Order by name
            vur = vur.OrderBy(o => o.DisplayName).ToList();


            IPagedList<ViewUsersRoles> paggedlist = vur.ToPagedList(page ?? 1, pageSize);
            

            Navigation nav = await _sharedService.NavigationAsync();
            nav.Search.Controller = "Settings";
            nav.Search.Action = "Users";

            ModelViewUser mvu = new()
            {
                Users = paggedlist,
                ModelToolBar = new ToolBar()
                {
                    Action = new List<ToolBarActionLink> {
                        new ToolBarActionLink {
                            Controller = "Settings",
                            Action = "Index",
                            Title = "Back",
                            Icon = "icon-reply"
                        },
                        new ToolBarActionLink {
                            Controller = "User",
                            Action = "Create",
                            Title = "Create",
                            Icon = "icon-plus"
                        }
                    }
                },
                Navigation = nav,
            };
            return View(mvu);
        }


        public ToolBar OnlyBack()
        {
            return new ToolBar()
            {
                Action = new List<ToolBarActionLink> { 
                    new ToolBarActionLink {
                        Controller = "Settings", 
                        Action = "Index", 
                        Title = "Back", 
                        Icon = "icon-reply" 
                    }
                }
            };
        }



        // NEW - Create User GET
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("Settings/Users/Create")]
        [Route("User/Create")]
        public async Task<IActionResult> UserCreate()
        {
            UserModel model = new() {
                Roles = await _ctx.Roles.OrderByDescending(o=>o.RoleId).ToListAsync(),
                Request = new UserCreateModel(),
                Navigation = new(),
                ToolBar = new ToolBar()
                {
                    Action = new List<ToolBarActionLink> {
                            new ToolBarActionLink {
                                Controller = "Settings",
                                Action = "Users",
                                Title = "Back",
                                Icon = "icon-reply"
                            },
                            new ToolBarActionLink {
                                JavaScript = "Save()",
                                Title = "Save",
                                Icon = "icon-floppy"
                            }
                        }
                }
            };
            return View("Create", model);
        }

        // NEW - Create User POST
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Settings/Users/Create")]
        public async Task<IActionResult> UserCreate([FromHeader] UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                UserModel userreq = new()
                {
                    Roles = await _ctx.Roles.ToListAsync(),
                    Request = model,
                    Navigation = new(),
                    ToolBar = new ToolBar()
                    {
                        Action = new List<ToolBarActionLink> {
                            new ToolBarActionLink {
                                Controller = "Settings",
                                Action = "Users",
                                Title = "Back",
                                Icon = "icon-reply"
                            },
                            new ToolBarActionLink {
                                JavaScript = "Save()",
                                Title = "Save",
                                Icon = "icon-floppy"
                            }
                        }
                    }
                };
                return View("Create", userreq);
            }

            //if (model.Password != model.ConfirmPassword)
            //{
            //    ViewBag.Error = "hesla nejsou shodna";
            //    model.Password = string.Empty;
            //    model.ConfirmPassword = string.Empty;
            //    return View(model);
            //}



            User newUser = new()
            {
                UserName = model.Name,
                DisplayName = model.Name,
                Email = model.Email,
                Created = DateTime.Now,
                IsActive = true
            };
            ReturnModel user = await _loginServices.Registration(newUser, model.Password);

            //User user = new()
            //{
            //    UserName = model.Name,
            //    DisplayName = model.Name,
            //    Email = model.Email,
            //    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            //    Created = DateTime.Now,
            //    IsActive = true
            //};

            //_ctx.Users.Add(user);
            //await _ctx.SaveChangesAsync();

            // NEW - přiřazení role uživateli
            UserRole userRole = new()
            {
                UserId = user.UserId,
                RoleId = model.RoleId
            };

            _ctx.UserRoles.Add(userRole);
            await _ctx.SaveChangesAsync();

            return RedirectToAction(nameof(Users)); //Index
        }

        // NEW - Create User GET
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("Settings/Users/Edit/{id}")]
        [Route("User/Edit/{id}")]
        public async Task<IActionResult> UserEdit(int id)
        {
            var user = await _ctx.Users.Include(i=>i.UserRoles).ThenInclude(i=>i.Role).FirstOrDefaultAsync(f => f.UserId == id);

            UserEditModel request = new()
            {
                UserId = user.UserId,
                Username = user.UserName,
                Name = user.DisplayName,
                Email = user.Email,
                RoleId = user.UserRoles.FirstOrDefault().RoleId,
            };

            UserModel model = new()
            {
                Roles = await _ctx.Roles.ToListAsync(),
                Edit = request,
                Navigation = new(),
                ToolBar = new ToolBar()
                {
                    Action = new List<ToolBarActionLink> {
                            new ToolBarActionLink {
                                Controller = "Settings",
                                Action = "Users",
                                Title = "Back",
                                Icon = "icon-reply"
                            },
                            new ToolBarActionLink {
                                JavaScript = "Save()",
                                Title = "Save",
                                Icon = "icon-floppy"
                            }
                        }
                }
            };
            return View("Edit", model);
        }


        // NEW - Create User POST
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("Settings/Users/Edit")]
        [Route("Settings/UsersEdit")]
        public async Task<IActionResult> EditCreate(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                UserModel userreq = new()
                {
                    Roles = await _ctx.Roles.ToListAsync(),
                    Edit = model,
                    Navigation = new(),
                    ToolBar = new ToolBar()
                    {
                        Action = new List<ToolBarActionLink> {
                            new ToolBarActionLink {
                                Controller = "Settings",
                                Action = "Users",
                                Title = "Back",
                                Icon = "icon-reply"
                            },
                            new ToolBarActionLink {
                                JavaScript = "Save()",
                                Title = "Save",
                                Icon = "icon-floppy"
                            }
                        }
                    }
                };
                return View("Edit", userreq);
            }

            User newUser = new()
            {
                UserName = model.Name,
                DisplayName = model.Name,
                Email = model.Email,
            };
            ReturnModel user = await _loginServices.Update(newUser, model.Password);

            // remove old values
            List<UserRole> userrole = _ctx.UserRoles.Where(w => w.UserId == user.UserId).ToList();
            _ctx.UserRoles.RemoveRange(userrole);


            UserRole userRole = new()
            {
                UserId = user.UserId,
                RoleId = model.RoleId
            };

            _ctx.UserRoles.Add(userRole);
            await _ctx.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }






        [Authorize]
        public ViewResult Claims()
        {
            IEnumerable<Claim> c = User?.Claims.Where(w=>w.Type.Contains(
                "groupsid") == false && w.Type.Contains("primarysid") == false && w.Type.Contains("denyonlysid") == false
                );



            ModelClaims mc = new() { 
                ToolBar = OnlyBack(),
                Claims = c,
                Navigation = new()
            };

            return View(mc);
        }


        

    }
}
