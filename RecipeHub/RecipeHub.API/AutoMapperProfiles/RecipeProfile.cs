using AutoMapper;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<NewRecipeDto, Recipe>();
            CreateMap<RecipeIngredientDto, RecipeIngredient>();
            CreateMap<Recipe, Recipe>()
                .ForMember(x => x.Pictures, opts => opts.Ignore())
                .ForMember(x => x.Comments, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
