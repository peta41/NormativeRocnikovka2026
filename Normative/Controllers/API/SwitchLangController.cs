using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;


namespace Normative.Controllers.api
{
    
    [ApiController]
    public class SwitchLangController : ControllerBase
    {
        [HttpGet]
        //[Route("api/Search/People/{SearchText?}")]
        [Route("api/[controller]/GetLanguage/{culture}")]
        public IActionResult GetLanguage(string culture)
        {
            
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            Console.WriteLine(Request.Headers.Host.ToString());

            return Ok(); //LocalRedirect(returnUrl);
            
        }
    }


    

}
