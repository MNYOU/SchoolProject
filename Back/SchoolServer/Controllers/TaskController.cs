using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;
using SchoolServer.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace SchoolServer.Controllers;

[Route("task/")]
[ApiController]
public class TaskController : Controller
{
    private readonly Random random;
    private readonly DataContext context;
    private readonly IDbRepository repository;
    private long UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

    private TaskModel taskModel; // переделать

    public TaskController(IDbRepository repository, Random random, DataContext context)
    {
        this.repository = repository;
        this.random = random;
        this.context = context;
    }

    [HttpGet]
    [Route("")]
    // [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 85")]
    public IActionResult Get([FromQuery] [Required] string subject)
    {
        var user = User;
        var taskDal = new TaskDal();
        var taskModel = new TaskModel(null);
        if (!User.IsInRole(Role.User.ToString()))
        {
            taskDal = context.Tasks
                .Where(x => x.SubjectDal.Name == subject)
                .Include(x => x.DifficultyDal)
                .Include(x => x.SubjectDal)
                .ToList()
                .MinBy(x => random.Next());
        }
        else
        {
            var completedTasks = context.Users.Include(x=>x.CompletedTasks).FirstOrDefault(x => x.Id == UserId)?.CompletedTasks;
            taskDal = context.Tasks
                .Where(x => x.SubjectDal.Name == subject)
                .Where(x => !completedTasks.Contains(x))
                .Include(x => x.DifficultyDal)
                .Include(x => x.SubjectDal)
                .ToList()
                .MinBy(x => Guid.NewGuid());
        }
        if (taskDal is null) return Json("по указанному предмету ничего не найдено");
        taskModel = new TaskModel(taskDal);
        return Json(taskModel.GetValueWithoutAnswer());
    }

    [HttpPut]
    [Route("check")]
    public async Task<IActionResult> CheckAnswer([FromBody] [Required] TaskModel task)
    {
        var taskDal = context.Tasks
            .Include(x => x.DifficultyDal)
            .Include(x => x.SubjectDal)
            .FirstOrDefault(x => x.Id == task.Id);
        var isCorrectAnswer = taskDal != null && taskDal.Answer == task.Answer;
        if (isCorrectAnswer && User.IsInRole(Role.User.ToString()))
        {
            context.Users.FirstOrDefault(x => x.Id == UserId && !x.CompletedTasks.Contains(taskDal))?.CompletedTasks.Add(taskDal);
            await context.SaveChangesAsync();
        }
        
        return Json(isCorrectAnswer);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadData()
    {
        // await FillUserDal();
        // await FillDataBase();
        return Json("difficulties");
    }

    [NonAction]
    private async Task<string> FillDataBase()
    {
        var path = "F:/загрузки яндекс/info_tasks.txt";
        var difficulties = context.Difficulties.ToList();
        var subjects = context.Subjects.ToList();
        using (FileStream fstream = System.IO.File.OpenRead(path))
        {
            var buffer = new byte[fstream.Length];
            // считываем данные
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            // декодируем байты в строку
            var textFromFile = Encoding.Default.GetString(buffer);
            var rows = textFromFile.Split("\r\n");
            // subject => <need_reform>
            // name
            // difficulty
            // desc
            // answer
            //Информатика
            // Программирование
            // Легкий 
            // Если a = 53, то результат a mod 10 равно
            // 3
            // empty
            var tasks = new List<TaskDal>();
            for (var i = 0; i < rows.Length; i+=6)
            {
                var dif = difficulties.FirstOrDefault(x => x.Name == rows[i + 2].ToLower());
                if (dif is null)
                {
                    foreach (var dal in difficulties)
                    {
                        Console.WriteLine($"{dal.Name} {rows[i+2].ToLower()}");
                    }

                    var s1 = difficulties.First(x => x.Name == "сложный");
                    var ty = 43;
                }
                if (rows[i].Contains("<need_reform>"))continue;
                tasks.Add(new TaskDal()
                {
                    SubjectDal = subjects.First(x => x.Name =="информатика"),
                    Name = rows[i + 1],
                    DifficultyDal = difficulties.First(x => rows[i+2].ToLower().Contains(x.Name)),
                    Description = rows[i+3],
                    Answer = rows[i+4]
                });
            }
            context.Tasks.AddRange(tasks);
            await context.SaveChangesAsync();
            return textFromFile;
        }
    }

    [NonAction]
    private async Task<string> FillUserDal()
    {
        var users = new List<UserDal>
        {
            new UserDal { Name = "arman", Login = "Arman14",Password = "unlz4aYRaV"},
            new UserDal { Name = "nikita", Login = "Nikita35",Password = "dhvVysi7gT"},
            new UserDal { Name = "anny", Login = "Anny53",Password = "lNhokfzs1H"},
            new UserDal { Name = "masha", Login = "Masha1",Password = "iIQiKbG7PX"},
            new UserDal { Name = "igor", Login = "Igor13",Password = "eALf1bjDud"},
            new UserDal { Name = "lana", Login = "Lana5",Password = "lhSNCWgSuR"},
            new UserDal { Name = "lauren", Login = "Lauren51",Password = "X6FXShibQ5"},
            new UserDal { Name = "gocha", Login = "Gocha643",Password = "RrPsTVdQaI"},
            new UserDal { Name = "igor", Login = "Igor13",Password = "eALf1bjDud"},
            new UserDal { Name = "marina", Login = "marina23",Password = "tuljHJvt07"},
            new UserDal { Name = "dmitriy", Login = "dmitriy52",Password = "cEZ69SXha3"},
            new UserDal { Name = "vladimir", Login = "vladimir51",Password = "M14wD2i0WC"},
        };
        var tasks = context.Tasks.ToList();
        for (var i = 0; i < users.Count; i++)
        {
            users[i].CompletedTasks.AddRange(tasks.OrderBy(x => random.Next()).Take(random.Next(1, tasks.Count)));
        }
        context.Users.AddRange(users);
        await context.SaveChangesAsync();
        return "users";
    }
}