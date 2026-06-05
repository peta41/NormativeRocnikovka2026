using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models;
using Normative.Models.Config;

namespace Normative.Services;

using System.Text.RegularExpressions;
using BCrypt.Net;
using Normative.Extensions;

public class LoginServices(NormativeContext context, IConfiguration configuration)
{
    private readonly NormativeContext _context = context;
    private readonly IConfiguration _configuration = configuration;
    private static bool PasswordValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        // Vysvětlení regexu:
        // ^(?=.*[a-z])   - musí obsahovat alespoň jedno malé písmeno
        // (?=.*[A-Z])    - musí obsahovat alespoň jedno velké písmeno
        // (?=.*\d)       - musí obsahovat alespoň jednu číslici (0-9)
        // (?=.*[@$!%*?&]) - musí obsahovat alespoň jeden speciální znak
        // [A-Za-z\d@$!%*?&]{8,32}$ - povolené znaky a délka od 8 do 32 znaků
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$";
 
       return Regex.IsMatch(password, pattern);
        //return true;
    }


    private string PasswordToHash(string password)
    {
        string pepper = _configuration.GetValue<string>("Pepper");
        return BCrypt.HashPassword($"{password}{pepper}");
    }

    internal async Task<ReturnModel> Login(LoginModel model)
    {
        User user = await _context.Users.Include(i => i.UserRoles).ThenInclude(r => r.Role)
            .ByUsernameOrEmail(model.UsernameOrEmail, model.UsernameOrEmail)
            .FirstOrDefaultAsync();

        ReturnModel returnModel = new();

        if (user == null) {
            returnModel.ErrorMessage = "Uknown user";
        }

        /// pass chech
        string pepper = _configuration.GetValue<string>("Pepper");
        bool validPass = BCrypt.Verify($"{model.Password}{pepper}", user.PasswordHash);


        if (!validPass) {
            returnModel.ErrorMessage = "Invalid password";
        }


        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, user.DisplayName ),
                    new Claim(ClaimTypes.GivenName, user.DisplayName),
                    new Claim(ClaimTypes.Email, user.Email),
        ];
        
        foreach (UserRole userrole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userrole.Role.Name));
        }


        returnModel.Claims = claims;

        return returnModel;
    }

    internal async Task<ReturnModel> Registration(User user, string pass)
    {
        User u = await _context.Users.ByUsernameOrEmail(user.Email, user.Email)
            .ByUsernameOrEmail(user.UserName, user.Email)
            .FirstOrDefaultAsync();

        // podminka pokracovat
        ReturnModel model = new();

        if (u != null)
        {
            //uzivatel uz existuje
            model.ErrorMessage = "uzivatel s timto jmenem nebo Emailem uz existuje.";
            return model;
        }

        //uzivatel neexistuje -> registrace
        if (!PasswordValid(pass))
        {
            model.ErrorMessage = "heslo nesplnuje pozadavky";
            return model;
        }

        //todo: funkce privatni email valid je to prakticky stejny akorat vymenim regulerni vyraz

        int userId = UpdateUser(user, pass);
        model.UserId = userId;
        model.Success = true;
        return model;
    }

    public async Task<ReturnModel> Update(User user, string pass)
    {
        User u = await _context.Users.ByUsernameOrEmail(user.Email, user.Email)
            .ByUsernameOrEmail(user.UserName, user.Email)
            .FirstOrDefaultAsync();

        // podminka pokracovat
        ReturnModel model = new();

        int userId = UpdateUser(user, pass);
        model.UserId = userId;
        model.Success = true;
        return model;
    }

    private int UpdateUser(User user, string pass) {
        if (user != null)
        {
            if (string.IsNullOrEmpty(pass) == false)
                user.PasswordHash = PasswordToHash(pass);

            if(user.UserId == 0)
            {
                user.Created = DateTime.Now;
            }

            _context.Update(user);
            _context.SaveChanges();
        }
        return user.UserId;
    }




}
