using Microsoft.AspNetCore.Mvc;
using UseManagementApi.Data;

namespace UseManagementApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("api/accounts/register")]
    public async Task<IActionResult> Register(
        [FromServices] ApiDbContext context,
        [FromBody] )
    {
        
    }
}