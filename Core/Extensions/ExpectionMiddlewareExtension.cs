using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ExpectionMiddlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
