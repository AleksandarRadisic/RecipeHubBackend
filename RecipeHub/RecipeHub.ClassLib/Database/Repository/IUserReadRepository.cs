using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository
{
    public interface IUserReadRepository : IBaseReadRepository<Guid, User>
    {
        public User GetByUsernameAndPassword(string username, string password);
        public User GetByUsername(string username);
        public User GetByEmail(string email);
    }
}
