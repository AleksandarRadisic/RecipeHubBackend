using AutoMapper;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<NewArticleDto, Article>();
            CreateMap<Article, Article>()
                .ForMember(x => x.Pictures, opts => opts.Ignore())
                .ForMember(x => x.Comments, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
