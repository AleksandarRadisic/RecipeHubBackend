using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Database.Infrastructure;
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
                switch (ex)
                {
                    case RegistrationException: return BadRequest(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
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
                switch (ex)
                {
                    case LogInException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Regular")]
        public IActionResult AuthorizationTest()
        {
            foreach (var claim in HttpContext.User.Claims)
            {
                if (claim.Type == ClaimTypes.GivenName)
                {
                    return Ok(claim.Value);
                }
            }
            return Ok(HttpContext.Request.Headers.Authorization);
        }
    }
}
