using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolServer.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{
    private long UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

    [HttpPost("create")]
    public IActionResult DoSomething()
    {
        return Json(new { name = "fgdffd", id = 1 });
    }

    [Authorize]
    // [Authorize(Roles = "User")]
    [HttpGet("get")]
    public IActionResult GetResult()
    {
        return Ok("you are in the system");
    }
}
