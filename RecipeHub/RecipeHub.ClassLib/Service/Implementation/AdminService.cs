using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class AdminService : BaseService, IAdminService
    {
        private IAdminService _adminServiceImplementation;

        public AdminService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public IEnumerable<User> GetSuspiciousUsers()
        {
            return _uow.GetRepository<IUserReadRepository>().GetSuspiciousUsers();
        }

        public void BanUser(Guid userId)
        {
            var user = _uow.GetRepository<IUserReadRepository>().GetById(userId);
            if (user == null) throw new EntityNotFoundException("User not found");
            user.Banned = true;
            _uow.GetRepository<IUserWriteRepository>().Update(user);
        }
    }
}
