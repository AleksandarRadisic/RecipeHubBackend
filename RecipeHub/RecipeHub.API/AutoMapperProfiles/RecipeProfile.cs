using AutoMapper;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<NewRecipeDto, Recipe>();
        }
    }
}
