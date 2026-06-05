using System.Security.Claims;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models;
using Normative.Models.Config;
using Normative.Services;

namespace Normative.Controllers;

public class AccountController(NormativeContext normativeContext, 
    IHttpContextAccessor httpContextAccessor,
    LoginServices loginServices) : Controller
{
    //temporaryUserAccount

    private readonly NormativeContext _context = normativeContext;
    private IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private LoginServices _loginServices = loginServices;

    [Authorize]
    public IActionResult Index()
    {
        return View(_context.Users.ToList());
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            if(model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "hesla nejsou shodna";
                model.Password = string.Empty;
                model.ConfirmPassword = string.Empty;
                return View(model);
            }


            User account = new()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                UserName = model.Username
            };


            ReturnModel returnModel = await _loginServices.Registration(account, model.Password);

            if (returnModel.Success == true) { 
                return RedirectToAction("Login", "Account");
            }


            // todo: error message / neni validni heslo, a uyival jiy existuje
            ViewBag.Error = returnModel.ErrorMessage;
            
        }
        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        ReturnModel returnModel = await _loginServices.Login(model);

        if (!string.IsNullOrEmpty(returnModel.ErrorMessage))
        {
            ViewBag.Error = returnModel.ErrorMessage;
        }
        else if (returnModel.Claims != null)
        {
            var claimsIdentity = new ClaimsIdentity(returnModel.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Přihlášení uživatele (vytvoření cookie)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );


            var x = _httpContextAccessor;

            // Přesměrování na zabezpečenou stránku
            return RedirectToAction("Index", "Home");/// todo: redirecturl


        }

        return View(model);
    }

    public IActionResult LogOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult SecurePage()
    {
        //tady jsem skocil ted budu delat views a video(35:24)
        ViewBag.Name = HttpContext.User.Identity.Name;
        return View();
    }
}
