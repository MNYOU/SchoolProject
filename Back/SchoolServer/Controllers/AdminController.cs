using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using SchoolServer.Models;

// [FromQuery] is to get values from the query string
// [FromRoute] is to get values from route data
// [FromForm] is to get values from posted form fields
// [FromBody] is to get values from the request body
// [FromHeader] is to get values from HTTP headers
// [FromService] will have value injected by the DI (Dependency Injection) resolver

namespace SchoolServer.Controllers;

[ApiController]
[Authorize(Roles = "Admin")] // Roles.Admin
[Route("admin")]
public class AdminController : Controller
{
    [HttpPost("data/load")]
    public IActionResult LoadData()
    {
        return StatusCode(501);
    }

    [HttpPost("tasks/info")]
    public IActionResult LoadTasks()
    {
        return StatusCode(501);
    }
}
// сперва почитать интернет
// добавить заполнение задач, предметов, сложностей, пользователей