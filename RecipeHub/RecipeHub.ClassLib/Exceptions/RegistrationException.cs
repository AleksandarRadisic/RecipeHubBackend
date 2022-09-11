using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.ClassLib.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string message) : base(message){}
    }
}
