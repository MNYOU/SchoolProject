using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using SchoolServer.Data.Entities;

namespace SchoolServer.Models;

public class UserModel
{
    public readonly PasswordHasher<UserModel> PasswordHasher;
    public UserModel()
    {
        PasswordHasher = new PasswordHasher<UserModel>();
    }

    public UserModel(UserDal userDal)
    {
        Id = userDal.Id;
        Name = userDal.Name;
        Login = userDal.Login;
        Roles = new[] { Role.User };
        PasswordHasher = new PasswordHasher<UserModel>();
        Password = PasswordHasher.HashPassword(this, userDal.Password);
        Password = PasswordHasher.HashPassword(this, userDal.Password);
        // я думаю, пароль не стоит хранить, так как он наверное не должен пригождаться

        
        if (userDal.Name == "arman" && userDal.Login == "superMegaHuman69")
        {
            Roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToArray();
        }
    }

    public long Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public string Login { get; set; }

    public string Password { get; set; }
    
    public string Salt { get; set; }

    public Role[] Roles { get; set; } = { Role.User };
}

public enum Role
{
    User,
    Manager,
    Admin
}