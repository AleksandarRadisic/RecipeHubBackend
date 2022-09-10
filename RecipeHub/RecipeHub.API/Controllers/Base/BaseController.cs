using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.ClassLib.Database.Infrastructure;

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
    }
}
