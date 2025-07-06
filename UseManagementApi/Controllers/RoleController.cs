using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;
using UseManagementApi.Models;
using UseManagementApi.ViewModels;
using  Microsoft.Extensions.Caching.Memory;
using UseManagementApi.Attributes;

namespace UseManagementApi.Controllers;

[ApiController]
[Authorize(Roles = "admin")]
[ApiKey]
public class RoleController : ControllerBase
{
    
    [HttpGet("api/roles")]
    public async Task<IActionResult> GetAsync(
        [FromServices] ApiDbContext context,
        [FromServices] IMemoryCache cache,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {

            var roles = await cache.GetOrCreateAsync(cache, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                return await context
                    .Roles
                    .AsNoTracking()
                    .Select(x => new ListRoleViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            });
            
            if (roles is null)
            {
                return NotFound(new ResultViewModel<List<ListRoleViewModel>>("Nenhum perfil foi encontrado..."));
            }   

            return Ok(new ResultViewModel<List<ListRoleViewModel>>(roles));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<ListRoleViewModel>>("Erro interno no servidor..."));
        }
    }

    [HttpGet("api/roles/{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] ApiDbContext context,
        [FromServices] IMemoryCache cache,
        [FromRoute] int id)
    {
        try
        {
            var role = await cache.GetOrCreateAsync(cache, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                return await context
                    .Roles
                    .AsNoTracking()
                    .Select(x => new ListRoleViewModel
                    {
                        Id = x.Id,
                        Name = x.Name

                    })
                    .FirstOrDefaultAsync(x => x.Id == id);
            });
                    
                    
                    
                    
                
            
            if (role is null)
            {
                return NotFound(new ResultViewModel<Role>("Perfil não encontrado..."));
            }

            return Ok(new ResultViewModel<ListRoleViewModel>(role));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Role>("Erro interno no servidor..."));
        }
    }

    [HttpPost("api/roles")]
    public async Task<IActionResult> PostAsync(
        [FromServices] ApiDbContext context,
        [FromBody] EditRoleViewModel model)
    {
        
        try
        {
            var role = new Role()
            {
                Name = model.Name
            };

            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Role>(role));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Role>("Erro interno no servidor..."));
        }
    }

    [HttpPut("api/roles/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id,
        [FromBody] EditRoleViewModel model)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role is null)
            {
                return NotFound(new ResultViewModel<Role>("Perfil não econtrado..."));
            }

            role.Name = model.Name;

            context.Roles.Update(role);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Role>(role));

        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Role>("Erro interno no servidor..."));
        }
    }

    [HttpDelete("api/roles/{id:int}")]
    async public Task<IActionResult> DeleteAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role is null)
            {
                return NotFound(new ResultViewModel<Role>("Perfil não encontrado..."));
            }
            
            context.Remove(role);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Role>(role));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Role>("Erro interno no servidor..."));
        }
        
    }
    

}