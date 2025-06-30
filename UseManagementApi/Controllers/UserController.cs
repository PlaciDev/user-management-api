using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;
using UseManagementApi.Models;
using UseManagementApi.ViewModels;
using SecureIdentity.Password;



namespace UseManagementApi.Controllers;

[Authorize(Roles = "admin")]
[ApiController]
public class UserController : ControllerBase
{

    [HttpGet("api/users")]
    public async Task<IActionResult> GetAsync(
        [FromServices] ApiDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var count = await context.Users.AsNoTracking().CountAsync();
            
            var users = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Role)
                .Select(x => new ListUserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    Role = new ListRoleViewModel
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name
                    }
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (users is null)
            {
                return NotFound(new ResultViewModel<List<User>>("Nenhum usuário encontrado..."));
            }

            return Ok(new ResultViewModel<List<ListUserViewModel>>(users));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<ListUserViewModel>>("Erro interno no servidor..."));
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
                return NotFound(new ResultViewModel<ListRoleViewModel>("Usuário não encontrado..."));
            }
            
            return Ok(new ResultViewModel<User>(user));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<User>("Erro interno no servidor..."));
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

            return Ok(new ResultViewModel<User>(user));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<User>("Erro interno no servidor..."));
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

            return Ok(new ResultViewModel<User>(user));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<User>("Erro interno no servidor..."));
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
                return NotFound(new ResultViewModel<User>("Nenhum usuário encontrado..."));
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<User>(user));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<User>("Erro interno no servidor..."));
        }
    }
    
}