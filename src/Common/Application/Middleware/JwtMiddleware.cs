using Application.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Middleware
{
    /// <summary>
    /// JWT token-based authentication middleware.
    /// Obtains the token from the header, validates that it is correct, and if so, checks a claim with the name of the environment. If the token contains said claim with the correct value, it is assigned to the context
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The middleware class must include:
        ///     - A public constructor with a parameter of type RequestDelegate.
        ///     - A public method called Invoke or InvokeAsync.This method must:
        ///         - Return Task.
        ///         - Accept a first parameter of type HttpContext.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IHostingEnvironment env, IConfiguration configuration)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachEnvironmentToContext(context, token, env, configuration);
            }
            await _next(context);
        }

        private async Task AttachEnvironmentToContext(HttpContext context, string token, IHostingEnvironment env, IConfiguration configuration)
        {
            await Task.Run(() =>
            {
                try
                {
                    // on successful jwt validation attach environment to context
                    var tokenEnvironment = TokenHelper.ValidateJwtToken(token, configuration);
                    if ((tokenEnvironment.Item1 == "Staging" && !env.IsProduction()) ||
                        (tokenEnvironment.Item1 == "Production" && env.IsProduction()))
                    {
                        context.Items["ValidEnvironment"] = true;
                        context.Items["UserName"] = tokenEnvironment.Item2;
                    }
                }
                catch
                {
                    // if jwt validation fails then do nothing 
                }
            });
        }
    }
}
