using RecipeHub.ClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Service.Implementation.Base;
using RecipeHub.ClassLib.Utilities;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IJwtGenerator _jwtGenerator;
        public AuthenticationService(IUnitOfWork uow, IJwtGenerator jwtGenerator, IMapper mapper) : base(uow, mapper)
        {
            _jwtGenerator = jwtGenerator;
        }

        public string[] LogIn(string username, string password)
        {
            var user = _uow.GetRepository<IUserReadRepository>().GetByUsernameAndPassword(username, PasswordEncoder.EncodePassword(password));
            if (user == null) throw new LogInException("User with given username and password not found!");

            return new [] { _jwtGenerator.GenerateToken(user), user.Role.Name, user.Id.ToString() };
        }

        public void Register(User user)
        {
            if (_uow.GetRepository<IUserReadRepository>().GetByUsername(user.UserName) != null)
                throw new RegistrationException("Username already taken");
            if (_uow.GetRepository<IUserReadRepository>().GetByEmail(user.Email) != null)
                throw new RegistrationException("Email already taken");
            user.Password = PasswordEncoder.EncodePassword(user.Password);
            user.RoleId = _uow.GetRepository<IRoleReadRepository>().getByName("Regular").Id;
            _uow.GetRepository<IUserWriteRepository>().Add(user);
        }

    }
}
