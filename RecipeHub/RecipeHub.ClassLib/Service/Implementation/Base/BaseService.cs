using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Infrastructure;

namespace RecipeHub.ClassLib.Service.Implementation.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
