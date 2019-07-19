using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareDemo.Middleware
{
    public class SecurityMiddleware
    {

        private RequestDelegate _next;

        public SecurityMiddleware (RequestDelegate next)
        {
            this._next = next;
        }

        public  async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<br/>This is from security middleware- Request");
            await _next.Invoke(context);
            await context.Response.WriteAsync("<br/>This is from security middleware- Response");

        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UserSecurity(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SecurityMiddleware>();
        }
    }
}
