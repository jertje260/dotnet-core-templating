using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Api.Middlewares.BasicAuthentication
{
    public static class BasicAuthenticationMiddlewareExtensionMethods
    {
        /// <summary>
        /// Adds the Basic Authentication Middleware,
        /// Use the `RegisterAuthentication` method to register the username & token used for the authentication
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBasicAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicAuthenticationMiddleware>();
        }

        /// <summary>
        /// Registers the username and token for the BasicAuthenticationMiddleware
        /// </summary>
        /// <param name="services"></param>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, string username,
            string token)
        {
            return services.AddSingleton(new BasicAuthenticationConfiguration(username, token));
        }
    }
}
