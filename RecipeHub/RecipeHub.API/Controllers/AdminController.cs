using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service;

namespace RecipeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IAdminService _adminService;
        public AdminController(IUnitOfWork uow, IMapper mapper, IConfiguration config, ICommentService commentService, IAdminService adminService) : base(uow, mapper, config)
        {
            _commentService = commentService;
            _adminService = adminService;
        }

        [HttpGet("reports/pending")]
        public IActionResult GetPendingReportedComments()
        {
            return Ok(_uow.GetRepository<ICommentReadRepository>().GetPendingReport(FetchType.Eager));
        }

        [HttpPost("comments/{id:guid}/report/respond")]
        public IActionResult RespondToCommentReport(ReportResponseDto dto, Guid id)
        {
            try
            {
                _commentService.RespondToReport(id, GetUserIdFromContext(), _mapper.Map<Report>(dto));
                return Ok("Report response saved successfully");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpGet("users/suspicious")]
        public IActionResult GetSuspiciousUsers()
        {
            try
            {
                return Ok(_adminService.GetSuspiciousUsers());
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPut("users/{id:guid}/ban")]
        public IActionResult BanUser(Guid id)
        {
            try
            {
                _adminService.BanUser(id);
                return Ok("User banned");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }
    }
}
