using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.ClassLib.Database.Infrastructure
{
    public interface IUnitOfWork
    {
        T GetRepository<T>();
    }
}
