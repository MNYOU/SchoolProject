using SchoolServer.Data.Entities;

namespace SchoolServer.Models;

public class Difficulty
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public Difficulty()
    {
        
    }

    public Difficulty(DifficultyDal difficultyDal)
    {
        Id = difficultyDal.Id;
        Name = difficultyDal.Name;
    }
}