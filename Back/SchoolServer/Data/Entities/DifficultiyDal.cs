﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolServer.Data.Entities;

[Table("difficulties")]
public record DifficultyDal : IEntityDal
{
    [Key]
    [Column("id")]
    public long Id { get; init; }
    
    [Column("name")]
    public string Name { get; init; }

    public List<TaskDal> Tasks { get; init; } = new();
}