using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;

namespace RecipeHub.ClassLib.Service.Implementation.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        protected const string RecipePictureDestination = "Pictures/Recipes";
        protected const string ArticlePrictureDestination = "Pictures/Articles";

        public BaseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}
