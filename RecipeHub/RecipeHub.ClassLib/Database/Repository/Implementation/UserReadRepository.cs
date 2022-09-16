using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class UserReadRepository : BaseReadRepository<Guid, User>, IUserReadRepository
    {
        public UserReadRepository(AppDbContext context) : base(context)
        {
        }

        public override User GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager)
            {
                return GetSet()
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == id);
            }

            return GetSet().Find(id);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return GetSet()
                .Include(user => user.Role)
                .FirstOrDefault(user => user.UserName.Equals(username) && user.Password.Equals(password));
        }

        public User GetByUsername(string username)
        {
            return GetSet().FirstOrDefault(user => user.UserName.Equals(username));
        }

        public User GetByEmail(string email)
        {
            return GetSet().FirstOrDefault(user => user.Email.Equals(email));
        }

        public IEnumerable<User> GetSuspiciousUsers(int numberOfBlockedComments = 3)
        {
            return GetSet()
                .Include(u => u.Comments.Where(c => c.Report != null && c.Report.BlockApproved))
                .Where(u => numberOfBlockedComments <= u.Comments.Count(c => c.Report != null && c.Report.BlockApproved));
        }
    }
}
