using Microsoft.AspNetCore.Mvc;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;
using Task = System.Threading.Tasks.Task;

namespace SchoolServer.Controllers;

[ApiController]
[Route("{controller}/{action}")]
public class Home : Controller
{
    private readonly IDbRepository repository;

    // private readonly data;
    public Home(IDbRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    // [Route("Index")]
    public IActionResult Index()
    {
        var tasks = repository.Get<DifficultyDal>();
        Task.Delay(1000);
        return Json(tasks.ToList());
        return Content("this request path is base");
    }
}