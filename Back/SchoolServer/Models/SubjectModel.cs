using SchoolServer.Data.Entities;

namespace SchoolServer.Models;

public class Subject
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public Subject()
    {
        
    }
    
    public Subject(SubjectDal subjectDal)
    {
        Id = subjectDal.Id;
        Name = subjectDal.Name;
    }
}