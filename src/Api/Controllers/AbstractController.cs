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
    public abstract class AbstractController<TEntity, TRepository, TEntityService, TDto>(TEntityService entityService) : ControllerBase where TEntity : class, IEntity where TRepository : class, IRepository<TEntity> where TEntityService : class, IEntityService<TEntity, TRepository> where TDto : class, IDto
    {
        protected readonly TEntityService _entityService = entityService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await _entityService.GetAllAsyncAsActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(string id)
        {
            var TEntity = await _entityService.GetByIdAsync(id);

            if (TEntity == null)
            {
                return NotFound();
            }

            return TEntity;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, TDto dto)
        {
            TEntity? entity = await _entityService.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            if (!entity.IsUserAuthorized(User.FindFirstValue(ClaimTypes.NameIdentifier))) // let it crash
            {
                return BadRequest();
            }



            entity.ApplyDto(dto);

            try
            {
                await _entityService.UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TEntity>> Post(TEntity TEntity)
        {
            await _entityService.CreateAsync(TEntity);

            return CreatedAtAction("Get", new { id = TEntity.Id }, TEntity);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _entityService.DeleteAsync(id);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        protected bool Exists(string id)
        {
            return _entityService.Exists(id);
        }
    }
}
