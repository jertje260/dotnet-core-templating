using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Configuration
{
    public class Api
    {
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
