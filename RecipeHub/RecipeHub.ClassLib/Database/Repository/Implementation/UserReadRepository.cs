using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class UserReadRepository : BaseReadRepository<Guid, User>, IUserReadRepository
    {
        public UserReadRepository(AppDbContext context) : base(context)
        {
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return GetSet().Include(user => user.Role).FirstOrDefault(user => user.UserName.Equals(username) && user.Password.Equals(password));
        }

        public User GetByUsername(string username)
        {
            return GetSet().FirstOrDefault(user => user.UserName.Equals(username));
        }

        public User GetByEmail(string email)
        {
            return GetSet().FirstOrDefault(user => user.UserName.Equals(email));
        }
    }
}
