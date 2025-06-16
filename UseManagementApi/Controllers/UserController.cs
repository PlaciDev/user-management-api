using Microsoft.AspNetCore.Mvc;
using UseManagementApi.Data;
using UseManagementApi.Models;


namespace UseManagementApi.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    
    [HttpPost("api/user")]
    public void Teste(
        [FromServices] ApiDbContext contex)
    {
        contex.Add(new User()
        {
            Id = 0,
            Name = "UserTeste",
            Email = "emailteste@gmail.com",
            PasswordHash = "1234",
            IsActive = true,
            Role = new Role()
            {
                Id = 0,
                Name = "RoleTeste"

            }


        });
    }
}