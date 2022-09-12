using AutoMapper;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<NewIngredientDto, Ingredient>();
        }
    }
}
