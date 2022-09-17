using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service;

namespace RecipeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public UsersController(IUnitOfWork uow, IMapper mapper, IConfiguration config, IAuthenticationService authenticationService) : base(uow, mapper, config)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(NewUserDto dto)
        {
            try
            {
                _authenticationService.Register(_mapper.Map<User>(dto));
                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpGet ("logged")]
        [Authorize]
        public IActionResult GetLoggedUser()
        {
            try
            {
                return Ok(_uow.GetRepository<IUserReadRepository>().GetById(GetUserIdFromContext()));
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
            
        }

        [HttpPost("login")]
        public IActionResult LogIn(LogInDto dto)
        {
            try
            {
                string[] retVal = _authenticationService.LogIn(dto.UserName, dto.Password);
                return Ok(new LoginResponseDto
                {
                    Token = retVal[0],
                });
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPut("password")]
        [Authorize]
        public IActionResult ChangePassword(PasswordChangeDto dto)
        {
            try
            {
                _authenticationService.ChangePassword(GetUserIdFromContext(), dto.Password);
                return Ok("Password changed");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
            
        }

        [HttpPut("UserInfo")]
        public IActionResult UpdateUserInfo(PersonalInfoUpdateDto dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);
                user.Id = GetUserIdFromContext();
                _authenticationService.UpdateUser(user);
                return Ok("Profile updated");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }

        }
    }
}
