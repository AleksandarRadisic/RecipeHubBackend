using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service
{
    public interface ICommentService
    {
        public void RespondToReport(Guid commentId, Guid userId, Report report);
    }
}
