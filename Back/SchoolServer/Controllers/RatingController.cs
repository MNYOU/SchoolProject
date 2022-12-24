using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using Xceed.Document.NET;

namespace SchoolServer.Controllers;

[ApiController]
[Authorize]
[Route("rating/")]
public class RatingController : Controller
{
    private readonly DataContext context;

    public RatingController(DataContext context)
    {
        this.context = context;
    }

    [AllowAnonymous]
    [HttpGet("global")]
    public IActionResult GetGlobalRating()
    {
        var difAnnotation = new Dictionary<string, long>
        {
            ["легкий"] = 1,
            ["средний"] = 2,
            ["сложный"] = 3
        };
        var users = context.Users
            .Include(u => u.CompletedTasks).ThenInclude(t => t.DifficultyDal).ToList()
            .OrderByDescending(x => x.CompletedTasks.Select(t => difAnnotation[t.DifficultyDal.Name]).Sum())
            .Select(x => new { x.Id, x.Name, Scores = x.CompletedTasks.Select(t => difAnnotation[t.DifficultyDal.Name]).Sum()});
        return Json(users);
    }
}