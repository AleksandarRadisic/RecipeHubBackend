using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto.PostPut
{
    public class ReportResponseDto
    {
        [Required(ErrorMessage = "Block decision required")]
        public bool BlockApproved { get; set; }
    }
}
