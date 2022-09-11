using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class UserProfile : RecipeProfile
    {
        public UserProfile()
        {
            CreateMap<NewUserDto, User>();
        }
    }
}
