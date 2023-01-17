using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolServer.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Не указан логин.")]
    // [EmailAddress]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Не указан пароль.")]
    // [PasswordPropertyText]
    public string Password { get; set; }
}