using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolServer.Data.Entities;

[Table("usersTasks")]
[Keyless]
public record CompletedTaskDal : IEntityDal
{
    [Column("userId")]
    public long UserId { get; init; }
    
    [ForeignKey(nameof(UserId))]
    public UserDal UserDal { get; init; }
    
    [Column("taskId")]
    public long TaskId { get; init; }
    
    [ForeignKey(nameof(TaskId))]
    public TaskDal TaskDal { get; init; }
}