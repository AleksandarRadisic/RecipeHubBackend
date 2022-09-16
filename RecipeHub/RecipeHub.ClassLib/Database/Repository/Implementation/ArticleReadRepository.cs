using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class ArticleReadRepository : BaseReadRepository<Guid, Article>, IArticleReadRepository
    {
        public ArticleReadRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Article> GetAll(FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager)
            {
                return GetSet()
                    .Include(a => a.Comments)
                    .Include(a => a.User)
                    .Include(a => a.Pictures)
                    .ToList();
            }

            return GetSet().ToList();
        }

        public Article GetById(Guid id, bool ignoreBlockedComments = true, FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager && !ignoreBlockedComments)
            {
                return GetSet()
                    .Include(a => a.Comments)
                    .ThenInclude(c => c.User)
                    .Include(a => a.User)
                    .Include(a => a.Pictures)
                    .FirstOrDefault(a => a.Id == id);
            }

            return GetById(id, fetchType);
        }

        public IEnumerable<Article> GetByUserId(Guid userId, FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager)
            {
                return GetSet()
                    .Include(a => a.Comments)
                    .ThenInclude(c => c.User)
                    .Include(a => a.User)
                    .Include(a => a.Pictures)
                    .Where(a => a.UserId == userId)
                    .ToList();
            }

            return GetSet().ToList();
        }

        public override Article GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager)
            {
                return GetSet()
                    .Include(a => a.Comments.Where(c => c.Report == null || !c.Report.BlockApproved))
                    .ThenInclude(c => c.User)
                    .Include(a => a.User)
                    .Include(a => a.Pictures)
                    .FirstOrDefault(a => a.Id == id);
            }
            return GetSet().Find(id);
        }
    }
}
