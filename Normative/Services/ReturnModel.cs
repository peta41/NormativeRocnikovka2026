using System.Security.Claims;

namespace Normative.Services;

public class ReturnModel
{
    public List<Claim> Claims {  get; set; }
    public int UserId { get; set; }
    public string ErrorMessage { get; set; }
    public bool Success { get; set; }
}
