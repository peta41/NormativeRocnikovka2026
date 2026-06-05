public class ForbiddenMiddleware
{
    private readonly RequestDelegate _next;



    public ForbiddenMiddleware(RequestDelegate next)
    {
        _next = next;
    }



    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        switch (context.Response.StatusCode)
        {
            case 403:
                context.Response.Redirect("/Home/Forbidden");
                break;
            case 404:
                context.Response.Redirect("/Home/PageNotFound");
                break;
        }

    }
}