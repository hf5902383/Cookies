using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookies.Models
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}