using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Template.Api.Middlewares.BasicAuthentication
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate             _next;
        private readonly BasicAuthenticationConfiguration _authConfiguration;

        public BasicAuthenticationMiddleware(RequestDelegate next,
            BasicAuthenticationConfiguration basicAuthenticationConfiguration)
        {
            _next = next;
            _authConfiguration = basicAuthenticationConfiguration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (Autheticated(context))
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
            }
        }

        private bool Autheticated(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("Authorization", out var authValues))
            {
                return ValidToken(authValues);
            }

            return false;
        }

        private bool ValidToken(string authValues)
        {
            if (string.IsNullOrEmpty(authValues)) return false;

            var matches = Regex.Match(authValues, "(Basic)( )(.*)");
            if (matches.Success)
            {
                var scheme = matches.Groups[1].Value;
                var parameter = matches.Groups[3].Value;

                if (scheme == "Basic")
                {
                    byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(_authConfiguration.Username + ":" + _authConfiguration.Token);
                    string configuredTokenBase64String = Convert.ToBase64String(encodedBytes);

                    if (configuredTokenBase64String == parameter)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
