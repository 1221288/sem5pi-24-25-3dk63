using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DDDSample1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClaimsController : ControllerBase
  {
    [HttpGet]
    public IActionResult GetClaims()
    {
      var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
      return Ok(claims);
    }
  }
}
