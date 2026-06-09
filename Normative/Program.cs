using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Services;
using System.Globalization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Database
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<NormativeContext>(options =>
        options.UseNpgsql(connectionString)
    );
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // Localization - VŽDY, bez podmínky
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");
    builder.Services.AddPortableObjectLocalization();

    // MVC - jeden øetìz, žádné duplicity
    builder.Services.AddControllersWithViews()
        .AddRazorRuntimeCompilation()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization()
        .AddNewtonsoftJson()
        .AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Add("/{0}.cshtml");
        });

    builder.Services.AddRazorPages();

    // Authentication
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "Normative.Cookie";
            options.LoginPath = "/Account/Login";
        });

    // Session
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromDays(365242);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
    });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<SharedService>();
    builder.Services.AddScoped<LoginServices>();

    // Localization options + .po soubory
    string resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Resource");

    if (Directory.Exists(resourcePath))
        builder.Services.AddHostedService<PoFileRefresherService>();

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new List<CultureInfo>();

        if (Directory.Exists(resourcePath))
        {
            foreach (var lang in new DirectoryInfo(resourcePath).GetFiles("*.po"))
                supportedCultures.Add(CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(lang.Name)));
        }

        if (!supportedCultures.Any())
            supportedCultures.Add(CultureInfo.GetCultureInfo("cs-CZ"));

        options.DefaultRequestCulture = new RequestCulture("cs-CZ", "cs-CZ");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRequestLocalization();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSession();

    app.MapRazorPages();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    throw;
}