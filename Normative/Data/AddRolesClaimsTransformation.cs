using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Config;
using System.Security.Claims;

namespace Normative.Data
{
    public class AddRolesClaimsTransformation(NormativeContext context) : IClaimsTransformation
    {

        private readonly NormativeContext _ctx = context;

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new();

            if (!principal.HasClaim(claim => claim.Type.Contains(ClaimTypes.UserData)))
            {
                // Actual user
                var user = principal.Identity.Name.ToLower().Trim().Split("\\")[1];


                //Get permmition from database
                User profile = _ctx.Users.FirstOrDefault(f => f.UserName.ToLower() == user);
                List<Role> roles = new();
                try
                {
                    roles = _ctx.Users.Where(w => w.UserId == profile.UserId)
                    .SelectMany(u => u.UserRoles).Select(ur => ur.Role).ToList();
                }
                catch
                {

                }
                

                if (profile != null)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, profile.DisplayName));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, profile.Email.ToLower()));

                    if (roles.Count != 0)
                    {
                        foreach (Role role in roles)
                        {
                            if (!claimsIdentity.HasClaim(claim => claim.Value.Contains(role.Name)))
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                            }
                        }
                    }
                    else
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Reader"));
                    }
                }
                else
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Anonymous"));
                }
            }


            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
