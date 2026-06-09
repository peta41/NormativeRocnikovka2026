using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Normative.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Normative.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);



    // Use NLog as logged system
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add Database connection
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<NormativeContext>(options =>
        options.UseSqlServer(connectionString)//.EnableSensitiveDataLogging(true)
    );

    // Catch database error message
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // Add MVC logic
    builder.Services.AddControllersWithViews()
        // Add runtime compilation 
        .AddRazorRuntimeCompilation()
        // Set folder for components
        .AddRazorOptions(options =>
        {
            // In the preceding code, the placeholder {0} represents the path Components/{View Component Name}/{View Name}
            options.ViewLocationFormats.Add("/{0}.cshtml");
        });


    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Normative.Cookie";
        options.LoginPath = "/Account/Login"; // Kam poslat nepøihlášeného uživatele
    });


    //temporaryUserAccount
    //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    //ENDtemporaryUserAccount



    //// Add authentification
    //builder.Services.AddAuthentication(options =>
    //{
    //    options.DefaultScheme = "CookieAuth";
    //    options.DefaultChallengeScheme = "CookieAuth";
    //})
    //    .AddCookie("CookieAuth", config =>
    //    {
    //        config.Cookie.Name = "Normative.Cookie";
    //        config.LoginPath = "/Home/Login";
    //        // config.AccessDeniedPath = "/Home/Forbidden"; // Doporuèuji lomítko na zaèátek
    //    })

    //    .AddNegotiate();

    builder.Services.AddRazorPages();

    // 3. Autorizace (Fallback policy znamená, že celá aplikace vyžaduje pøihlášení, pokud neøekneš jinak)
    //builder.Services.AddAuthorization(options =>
    //{
    //    options.FallbackPolicy = options.DefaultPolicy;
    //});
    //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

    //builder.Services.AddAuthorization(options =>
    //{
    //    // By default, all incoming requests will be authorized according to the default policy.
    //    options.FallbackPolicy = options.DefaultPolicy;
    //});

    // Own identity
    //builder.Services.AddScoped<IClaimsTransformation, AddRolesClaimsTransformation>();

    // Add SharedService
    builder.Services.AddScoped<SharedService>();
    builder.Services.AddHttpContextAccessor();

    //// Quick message
    //builder.Services.AddScoped<QuickMessage>();
    //builder.Services.AddScoped<IQuickMessage<Controller>, QuickMessage<Controller>>();
    //builder.Services.AddScoped(typeof(IQuickMessage<>), typeof(QuickMessage<>));

    // Add session
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromDays(365242);
        options.Cookie.IsEssential = true;
        // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        // options.Cookie.SameSite = SameSiteMode.Strict; 
        options.Cookie.HttpOnly = true;
    });
    builder.Services.AddDistributedMemoryCache();

    //builder.Services.AddScoped<IClaimsTransformation, AddRolesClaimsTransformation>();

    // Support Multi-Language
    string path = Directory.GetCurrentDirectory() + @"\Resource\";
    if (Directory.Exists(path))
    {
        builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
        builder.Services.AddPortableObjectLocalization();
        builder.Services.AddHostedService<PoFileRefresherService>();
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            // Load Language File from Folder
            var supportedCultures = new List<CultureInfo>();
            string path = Directory.GetCurrentDirectory() + @"\Resource\";
            if (Directory.Exists(path))
            {
                DirectoryInfo d = new(path);
                List<FileInfo> languages = d.GetFiles("*.po").ToList();

                foreach (FileInfo lang in languages)
                {
                    supportedCultures.Add(CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(lang.Name)));
                }
            }
            else
            {
                supportedCultures.Add(CultureInfo.GetCultureInfo("cs-CZ"));
            }

            options.DefaultRequestCulture = new RequestCulture(culture: "cs-CZ", uiCulture: "cs-CZ");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

        });

        builder.Services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

        builder.Services.AddControllers().AddNewtonsoftJson();

    }

    builder.Services.AddScoped<LoginServices>();

    var app = builder.Build();


    //// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    // Custom error page
    //app.UseMiddleware<ForbiddenMiddleware>();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    //temporaryUserAccount
    app.UseAuthentication();
    //ENDtemporaryUserAccount

    app.UseAuthorization();


    app.UseSession();
    app.MapRazorPages();


    app.UseRequestLocalization();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}