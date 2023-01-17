using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;
using SchoolServer.Models;

namespace SchoolServer.Controllers;

[Route("task/")]
[ApiController]
public class TaskController : Controller
{
    private readonly Random random;
    private readonly DataContext context;
    private readonly IDbRepository repository;
    private long UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value); // не робит

    private TaskModel taskModel; // переделать

    public TaskController(IDbRepository repository, Random random, DataContext context)
    {
        this.repository = repository;
        this.random = random;
        this.context = context;
    }

    [HttpGet("{subject}")]
    public IActionResult Get([FromRoute] [Required] string subject)
    {
        var taskDal = new TaskDal();
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

        return Json(taskDal is null ? false : new TaskModel(taskDal).GetValueWithoutAnswer());
    }

    [HttpPut("check")]
    public async Task<IActionResult> CheckAnswer([FromBody] [Required] TaskModel task)
    {
        var headers = HttpContext.Request.Headers.Authorization;
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
        await FillAlgebra();
        // var text = await FillUserDalCorrect();
        // await FillUserDal();
        // await FillDataBase();
        return Ok();
    }

    private async Task FillAlgebra()
    {
        var fileName = "geometr.txt";
        // если не кастить к листу, то каждый раз будет производиться запрос к базе данных
        var difficulties = context.Difficulties.ToList();
        var subjects = context.Subjects.ToList();
        using StreamReader readtext = new StreamReader($"projectData/{fileName}");
        var text = await readtext.ReadToEndAsync();
        var rows = text.Split("\r\n");
        var tasks = new List<TaskDal>();
        for (var i = 0; i < rows.Length; i+=5)
        {
            if (rows[i].Contains("<need_reform>"))continue;
            tasks.Add(new TaskDal()
            {
                SubjectDal = subjects.First(x => x.Name =="математика"),
                Name = rows[i + 1],
                DifficultyDal = difficulties.First(x => rows[i+2].ToLower().Contains(x.Name)),
                Description = rows[i+3],
                Answer = rows[i+4]
            });
        }
        context.Tasks.AddRange(tasks);
        // await context.SaveChangesAsync();
    }

    [NonAction]
    private async Task<string> FillDataBase()
    {
        var path = "projectData/info.txt";
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

    [NonAction]
    private async Task<string> FillUserDalCorrect()
    {
        var isFileWrite = false;
        if (isFileWrite)
        {
            await using StreamWriter writetext = new StreamWriter("users.txt");
            var groups = new Dictionary<string, int>
            {
                ["7a"] = 27,
                ["7b"] = 29,
                ["7v"] = 22,
                ["7g"] = 27,
            };
            // user7a1
            var users = new List<string>();
            var text = new StringBuilder();
            foreach (var group in groups)
            {
                for (var i = 1; i < group.Value + 1; i++)
                {
                    var user = new UserDal()
                    {
                        Login = $"user{group.Key}{i}",
                        Name = $"defaultName{i}", // некорректно
                        Password = GetRandomAlphanumericString(10)
                    };
                    text.Append($"{user.Login} {user.Password}\n");
                }
            }
             
            await writetext.WriteAsync(text);
        }

        var ifFileRead = true;
        if (ifFileRead)
        {
            using StreamReader readtext = new StreamReader("users.txt");
            var text = await readtext.ReadToEndAsync();
            var groups = new Dictionary<string, int>
            {
                ["7a"] = 27,
                ["7b"] = 29,
                ["7v"] = 22,
                ["7g"] = 27,
            };
            // user7a1
            var users = new List<UserDal>();
            var builder = new StringBuilder();
            var number = 1;
            foreach (var line in text.Split('\n'))
            {
                if (line.Split(' ').Length != 2)
                    continue;
                var user = new UserDal()
                {
                    Login = line.Split(' ')[0],
                    Password = line.Split(' ')[1],
                    Name = $"defaultName{number}"
                };
                var hasher = new PasswordHasher<UserDal>();
                var hashed = hasher.HashPassword(user, user.Password);
                user = new UserDal() {Name = user.Name, Login = user.Login, Password = hashed};
                users.Add(user);
                number++;
            }

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        return "";
    }
    
    private static string GetRandomAlphanumericString(int length)
    {
        const string alphanumericCharacters =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789";
        return GetRandomString(length, alphanumericCharacters);
    }

    private static string GetRandomString(int length, IEnumerable<char> characterSet)
    {
        if (length < 0)
            throw new ArgumentException("length must not be negative", "length");
        if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
            throw new ArgumentException("length is too big", "length");
        if (characterSet == null)
            throw new ArgumentNullException("characterSet");
        var characterArray = characterSet.Distinct().ToArray();
        if (characterArray.Length == 0)
            throw new ArgumentException("characterSet must not be empty", "characterSet");

        var bytes = new byte[length * 8];
        var result = new char[length];
        using (var cryptoProvider = new RNGCryptoServiceProvider())
        {
            cryptoProvider.GetBytes(bytes);
        }
        for (int i = 0; i < length; i++)
        {
            ulong value = BitConverter.ToUInt64(bytes, i * 8);
            result[i] = characterArray[value % (uint)characterArray.Length];
        }
        return new string(result);
    }
}