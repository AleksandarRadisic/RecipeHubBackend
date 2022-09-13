using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Exceptions;

namespace RecipeHub.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;

        public BaseController(IUnitOfWork uow, IMapper mapper, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            _config = config;
        }

        protected Guid GetUserIdFromContext()
        {
            try
            {
                foreach (var claim in HttpContext.User.Claims)
                {
                    if (claim.Type == "id")
                    {
                        return Guid.Parse(claim.Value);
                    }
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
            
        }

        protected IActionResult ReturnErrorResult(Exception ex)
        {
            switch (ex)
            {
                case ArgumentException: return BadRequest(ex.Message);
                case RegistrationException: return BadRequest(ex.Message);
                case AlreadyExistsException: return BadRequest(ex.Message);
                case UnauthorizedAccessException: return Unauthorized(ex.Message);
                case EntityNotFoundException: return NotFound(ex.Message);
                case LogInException: return NotFound(ex.Message);
                case ForbiddenException: return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
                case BannedException: return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
                default: return Problem("Oops, something went wrong. Try again");
            }
        }

    }
}
