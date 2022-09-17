using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class UserProfile : RecipeProfile
    {
        public UserProfile()
        {
            CreateMap<NewUserDto, User>();
            CreateMap<PersonalInfoUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, User>()
                .ForMember(u => u.RoleId, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
