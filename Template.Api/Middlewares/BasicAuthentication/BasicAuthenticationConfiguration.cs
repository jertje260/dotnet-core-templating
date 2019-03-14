using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Api.Middlewares.BasicAuthentication
{
    public class BasicAuthenticationConfiguration
    {
        public BasicAuthenticationConfiguration(string username, string token)
        {
            Username = username;
            Token = token;
        }
        public readonly string Username;
        public readonly string Token;
    }
}
