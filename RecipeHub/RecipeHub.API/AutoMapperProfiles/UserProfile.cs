using RecipeHub.API.Dto.PostPut;
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
