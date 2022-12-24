using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolServer.Models;

public class LoginModel
{
    [Required]
    // [EmailAddress]
    public string Login { get; set; }
    
    [Required]
    // [PasswordPropertyText]
    public string Password { get; set; }
}