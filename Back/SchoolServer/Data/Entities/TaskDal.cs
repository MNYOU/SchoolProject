using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolServer.Migrations;

namespace SchoolServer.Data.Entities;

[Table("tasks")]
public record TaskDal : IEntityDal
{
    [Key] [Column("id")] public long Id { get; init; }
    
    [Column("name")] public string Name { get; init; }

    [Column("description")] public string Description { get; init; }
    
    [Column("difficultyId")]
    public long DifficultyId { get; init; }

    [ForeignKey(nameof(DifficultyId))]
    public DifficultyDal DifficultyDal { get; init; }

    [Column("subjectId")]
    public long SubjectId { get; init; }
    
    [ForeignKey(nameof(SubjectId))]
    public SubjectDal SubjectDal { get; init; }
    
    [Column("answer")]public string Answer { get; init; }
    
    public List<UserDal> Users { get; init; } = new();
    // public List<CompletedTaskDal> CompletedTaskList { get; init; } = new();
}