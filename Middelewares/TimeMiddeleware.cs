public class TimeMiddeleware
{

    readonly RequestDelegate next;

    public TimeMiddeleware(RequestDelegate nextRequest)
    {
        next = nextRequest;
    }

    public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
    {
        await next(context);

        if (context.Request.Query.Any(p => p.Key == "time"))
        {
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        }
    }

}

public static class TimeMiddelewareExtension
{
    public static IApplicationBuilder UseTimeMiddeleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeMiddeleware>();
    }
}