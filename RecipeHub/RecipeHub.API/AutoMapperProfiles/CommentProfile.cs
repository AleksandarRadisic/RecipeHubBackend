using AutoMapper;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<NewCommentDto, Comment>();
        }
    }
}
