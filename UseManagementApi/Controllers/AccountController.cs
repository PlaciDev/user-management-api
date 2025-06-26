using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using UseManagementApi.Data;
using UseManagementApi.Models;
using UseManagementApi.ViewModels;

namespace UseManagementApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("api/accounts/register")]
    public async Task<IActionResult> Register(
        [FromServices] ApiDbContext context,
        [FromBody] RegisterViewModel model)
    {
        var defaultRole = context.Roles.FirstOrDefault(x => x.Name == "commonUser");
        
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = PasswordHasher.Hash(model.Password),
            Role = defaultRole

        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, password = PasswordHasher.Hash(model.Password)
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Esse email já esta cadastrado..."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor..."));
        }
    }
}