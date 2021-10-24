using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Entity
{
    public class VerifyLogin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public VerifyLogin(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public bool VerifyLoginCredentials()
        {
            string name = _session.GetString("username");
            string password = _session.GetString("password");
            return name != null && password != null;
        }

        public void AddLoginCredentials(LoginCredentials loginCredentials)
        {
            _session.SetString("username", loginCredentials.Username);
            _session.SetString("password", loginCredentials.Password);
        }
    }
}
