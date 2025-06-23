using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;
using UseManagementApi.Models;
using UseManagementApi.ViewModels;

namespace UseManagementApi.Controllers;

[ApiController]
public class RoleController : ControllerBase
{
    [HttpGet("api/roles")]
    public async Task<IActionResult> GetAsync(
        [FromServices] ApiDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var count = await context.Roles.CountAsync();
            
            var roles = await context
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
            

            if (roles is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }

            return Ok(roles);
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpGet("api/roles/{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id)
    {
        try
        {
            var role = await context
                .Roles
                .AsNoTracking()
                .Select(x => new ListRoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                    
                })
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }

            return Ok(role);
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpPost("api/roles")]
    public async Task<IActionResult> PostAsync(
        [FromServices] ApiDbContext context,
        [FromBody] EditRoleViewModel model)
    {
        try
        {
            var role = new Role
            {
                Name = model.Name
            };

            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
            
            return Ok(role);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Erro interno no servidor...");
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
                return NotFound("Nenhum perfil encontrado...");
            }

            role.Name = model.Name;

            context.Roles.Update(role);
            await context.SaveChangesAsync();
            
            return Ok(role);
            
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
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
                return NotFound("Nenhum perfil encontrado...");
            }
            
            context.Remove(role);
            await context.SaveChangesAsync();
            
            return Ok(role);
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
        
    }
    

}