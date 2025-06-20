using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;
using UseManagementApi.Models;
using UseManagementApi.ViewModels;
using SecureIdentity.Password;



namespace UseManagementApi.Controllers;

[ApiController]
public class UserController : ControllerBase
{

    [HttpGet("api/users")]
    public async Task<IActionResult> GetAsync(
        [FromServices] ApiDbContext context)
    {
        try
        {
            var users = await context.Users.AsNoTracking().ToListAsync();

            if (users is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }
            
            return Ok(users);
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpGet("api/users/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id)
    {
        try
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }
            
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpPost("api/users")]
    public async Task<IActionResult> PostAsync(
        [FromServices] ApiDbContext context,
        [FromBody] EditUserViewModel model)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == model.RoleName);
            
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = PasswordHasher.Hash(model.Password),
                IsActive = true,
                CreatedAt = DateTime.Now,
                Role = role
                
            };
            
            
            
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            
            return Ok(user);
        }
        catch
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpPut("api/users/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id,
        [FromBody]  EditUserViewModel model)
    {
        try
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == model.RoleName);
            
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.PasswordHash = PasswordHasher.Hash(model.Password);
            user.IsActive = model.IsActive;
            user.Role = role;
            
            context.Users.Update(user);
            await context.SaveChangesAsync();
            
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }

    [HttpDelete("api/users/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] ApiDbContext context,
        [FromRoute] int id)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return NotFound("Nenhum perfil encontrado...");
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            
            return Ok(user);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Erro interno no servidor...");
        }
    }
    
}