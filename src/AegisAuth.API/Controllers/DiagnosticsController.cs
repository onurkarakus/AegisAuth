using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AegisAuth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticsController : ControllerBase
{
    [HttpGet("health")]
    [Authorize(Policy = "RequireApiReadScope")]
    public IActionResult Health()
    {
        var claims = User.Claims.Select(c => new Dictionary<string, string> { { "Type", c.Type }, { "Value", c.Value } }).ToList();

        return Ok(new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            UserClaims = claims
        });
    }
}
