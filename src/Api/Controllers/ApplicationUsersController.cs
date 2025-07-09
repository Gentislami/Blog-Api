using Blog_Api.src.Dtos;
using Blog_Api.src.Entities;
using Blog_Api.src.Repositories;
using Blog_Api.src.Services.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog_Api.src.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController(IApplicationUserEntityService applicationUserEntityService) : ControllerBase
    {
        protected readonly IApplicationUserEntityService _entityService = applicationUserEntityService;

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser?>> Get(string id)
        {
            if (id == User.FindFirstValue(ClaimTypes.NameIdentifier)) // let it crash
            {
                return BadRequest();
            }

            var ApplicationUser = await _entityService.GetByIdAsync(id);

            return ApplicationUser;
        }

        protected bool Exists(string id)
        {
            return _entityService.Exists(id);
        }

    }
}
