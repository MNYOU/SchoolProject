using System.Net.Mime;
using SchoolServer.Data.Entities;

namespace SchoolServer.Models;

public class TaskModel
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public string Difficulty { get; set; }

    public string Subject { get; set; }
    public string Answer { get; set; }

    // public TaskModel(TaskDal taskDal)
    // {
    //     if (taskDal is null) return;
    //     Id = taskDal.Id;
    //     Name = taskDal.Name;
    //     Description = taskDal.Description;
    //     Difficulty = new Difficulty(taskDal.DifficultyDal);
    //     Subject = new Subject(taskDal.SubjectDal);
    //     Answer = taskDal.Answer;
    // }

    public TaskModel()
    {
        
    }
    
    public TaskModel(TaskDal taskDal)
    {
        if (taskDal is null) return;
        Id = taskDal.Id;
        Name = taskDal.Name;
        Description = taskDal.Description;
        Difficulty = taskDal.DifficultyDal.Name;
        Subject = taskDal.SubjectDal.Name;
        Answer = taskDal.Answer;
    }

    public object GetValueWithoutAnswer()
    {
        return new
        {
            id = Id, name = Name, description = Description, difficulty = Difficulty, subject = Subject,
            answer = ""
        };
    }
}