using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;

namespace UseManagementApi.Controllers;

[ApiController]
public class RoleController : ControllerBase
{
    public async Task<IActionResult> GetAsync(
        [FromServices] ApiDbContext context)
    {
        try
        {
            var role = context.Roles.AsNoTracking().ToListAsync();

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
    
    
}