using AutoMapper;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.AutoMapperProfiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportResponseDto, Report>()
                .BeforeMap((s, d) => d.AdminConfirmed = true);
        }
    }
}
