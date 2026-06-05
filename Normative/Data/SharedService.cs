using Microsoft.EntityFrameworkCore;
using Normative.Models.Config;
using Normative.Models.Screen;

namespace Normative.Data
{
    public class SharedService
    {
        private readonly NormativeContext _ctx;
        private readonly ILogger<SharedService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public SharedService(NormativeContext context, ILogger<SharedService> logger, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _ctx = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public async Task<User> My()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;
            

            var claimsEmail = claims.FirstOrDefault(w => w.Type.Contains("email"));

            if (claimsEmail == null) return null;
            string email = claimsEmail.Value;

            
            return await _ctx.Users.Where(w => w.Email == email.ToString()).FirstOrDefaultAsync();
        }


        public bool Logged()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;


            var claimsRole = claims.FirstOrDefault(w => w.Type.Contains("role"));

            if (claimsRole == null) return false;
            var role = claimsRole.Value;

            return role != "Anonymous";
        }






        //public FieldList Permissions(int headerId, string username)
        //{
        //    List<ViewPermissions> per = _ctx.GetPermissions(username, headerId);

        //    FieldList fields = new();

        //    foreach(Field field in fields.Fields) 
        //    {
        //        int i = per.Where(f => f.Field == field.Name).Count() > 0 ? (int)per.First(f => f.Field == field.Name).Access : 1;
        //        field.Writable = i == 2;
        //    }

        //    return fields;
        //}

        //public Privileges GetPrivileges(int headerId, string username)
        //{
        //    List<ViewPermissions> per = _ctx.GetPermissions(username, headerId);

        //    Privileges privileges = new();

        //    foreach (ViewPermissions item in per)
        //    {
        //        privileges.Set(item.Field, item.Access);
        //    }

        //    return privileges;
        //}

        public async Task<Navigation> NavigationAsync()
        {
            //List<Site> sites = await _ctx.Sites.Where(w => w.IsActive == true).ToListAsync();
            //List<Department> departments = await _ctx.Departments.Where(w => w.IsActive == true).ToListAsync();

            //List<State> states = new();

            //foreach (Stage state in System.Enum.GetValues(typeof(Stage)))
            //{
            //    states.Add(GetStatus.ByNumber((int)state));
            //}
            //states.Remove(states.Where(w => w.Name == "N/A").FirstOrDefault());

            //List<UserView> viewslist = null;
            //if (_httpContextAccessor.HttpContext.User != null)
            //{
            //    string username = _httpContextAccessor.HttpContext.User?.Identity.Name.ToLower().Trim().Split("\\")[1];
            //    User user = await _ctx.Users.Where(w => w.UserName.ToLower() == username).FirstOrDefaultAsync();
            //    viewslist = _ctx.UserViews.Where(w => w.UserId == user.UserId).ToList();
            //}

            //List<Category> categories = await _ctx.Categories.Where(w => w.IsActive == true).ToListAsync();

            return new Navigation()
            {
                //SiteList = sites,
                //DepartmentList = departments,
                //CategoryList = categories,
                Search = new Search { Controller = "Home", Action = "Index" },
                //Status = states,
                //UserViewList = viewslist
            };
        }


    }
}
