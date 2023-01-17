using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using SchoolServer.Data.Entities;

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

    [HttpGet("self/")]
    public IActionResult GetUserRating([FromQuery] string? subject)
    {
        var login = User.FindFirst(x => x.Type == ClaimTypes.Email);
        var users = context.Users
            .Include(u => u.CompletedTasks.Where(t => subject == null || t.SubjectDal.Name == subject))
            .ThenInclude(t => t.DifficultyDal)
            .ToList();

        var user = SortUserDals(users)
            .Select((u, i) => new
                {
                    Place = i + 1,
                    User = u,
                    Scores = u.CompletedTasks.Select(t => t.DifficultyDal.Score).Sum() 
                })
            .FirstOrDefault(x => x.User.Login == login?.Value);
        if (user is null) return StatusCode(520);
        return Json(new { user.Place, user.User.Id, user.User.Login, user.Scores });
    }

    [HttpGet("global/")]
    public IActionResult GetGlobalRating([FromQuery] int? count)
    {
        var users = context.Users
            .Include(u => u.CompletedTasks)
            .ThenInclude(t => t.DifficultyDal)
            .ToList();

        return Json(GetUsersForRating(SortUserDals(users), count ?? 100));
    }

    [HttpGet("subjects/{subject}")]
    public IActionResult GetSubjectRating([FromRoute] string subject, [FromQuery] int? count)
    {
        var users = context.Users
            .Include(u => u.CompletedTasks.Where(t => t.SubjectDal.Name == subject))
            .ThenInclude(t => t.DifficultyDal)
            .ToList();

        return Json(GetUsersForRating(SortUserDals(users),count ?? 100));
    }

    [NonAction]
    private List<UserDal> SortUserDals(List<UserDal> users)
    {
        return users
            .OrderByDescending(u => u.CompletedTasks.Select(t => t.DifficultyDal.Score).Sum())
            .ToList();
    }

    [NonAction]
    private IEnumerable<object> GetUsersForRating(List<UserDal> users, int count)
    {
        // на данный момент используются логины, что противоречиво
        return users
            .Where(u => u.CompletedTasks.Select(t => t.DifficultyDal.Score).Sum() != 0)
            .Take(count)
            .Select((x, i) => new
                { x.Id, Place = i + 1, x.Login, Scores = x.CompletedTasks.Select(t => t.DifficultyDal.Score).Sum() });
    }
}