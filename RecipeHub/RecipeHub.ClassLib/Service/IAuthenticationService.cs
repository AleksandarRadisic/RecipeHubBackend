using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service
{
    public interface IAuthenticationService
    {
        public void Register(User user);
        public string[] LogIn(string username, string password);
    }
}
