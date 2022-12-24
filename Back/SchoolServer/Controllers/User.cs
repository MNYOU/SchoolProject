using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolServer.Controllers;

[ApiController]
[Route("user")]
// public class User: BaseController
public class User : Controller
{
    [HttpGet]
    [Route("")]
    public IActionResult Home()
    {
        // Response.WriteAsync("nobody can't do it");
        // return Content("soso so so so");
        return Ok("some response");
    }

    [HttpGet]
    [Route("get")]
    public IActionResult Index()
    {
        return Content("so so so so");
    }


    [HttpGet]
    [Route("state")]
    public IActionResult State()
    {
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("auto")]
    public IActionResult NeedAuto()
    {
        // Response.Redirect("api");

        // return View("home.html");
        // Response.Redirect(Request.Path.Value.Split("user")[0] + "list.html");
        // Response.ContentType 
        return Content("i dont know about it, but this page use authorize");
    }
}