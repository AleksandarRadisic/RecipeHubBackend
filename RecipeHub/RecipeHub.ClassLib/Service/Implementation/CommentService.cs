using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public void RespondToReport(Guid commentId, Guid userId, Report report)
        {
            if (_uow.GetRepository<IUserReadRepository>().GetById(userId, FetchType.Eager).Role.Name != "Admin")
                throw new ForbiddenException("Only admins can respond to report");
            Comment comment = _uow.GetRepository<ICommentReadRepository>().GetById(commentId);
            if (comment == null) throw new EntityNotFoundException("Comment not found");
            if (comment.Report == null) throw new ForbiddenException("This comment is not reported");
            if (comment.Report.AdminConfirmed)
                throw new ForbiddenException("Admin has already responded to this report");
            report.AdminConfirmed = true;
            comment.Report = report;
            _uow.GetRepository<ICommentWriteRepository>().Update(comment);
        }
    }
}
