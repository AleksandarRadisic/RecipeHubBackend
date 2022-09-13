using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public IEnumerable<User> GetSuspiciousUsers()
        {
            return _uow.GetRepository<IUserReadRepository>().GetSuspiciousUsers();
        }
    }
}
