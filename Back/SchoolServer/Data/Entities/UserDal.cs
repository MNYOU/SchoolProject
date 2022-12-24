using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolServer.Data.Entities;

[Table("users")]
public record UserDal : IEntityDal
{
    [Key]
    [Column("id")]
    public long Id { get; init; }

    [Column("name")]
    public string Name { get; init; }
    
    [Column("login")]
    public string Login { get; init; }

    [Column("password")]
    public string Password { get; init; }

    public List<TaskDal> CompletedTasks { get; init; } = new();
    // public List<CompletedTaskDal> CompletedTaskList { get; init; } = new();

}