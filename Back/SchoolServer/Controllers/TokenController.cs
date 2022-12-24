using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolServer.Data;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;
using SchoolServer.Models;

namespace SchoolServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : Controller
{
    private readonly IConfiguration config;
    private readonly DataContext context;
    private readonly DbRepository repository;

    public TokenController(IConfiguration config, DbRepository repository, DataContext context)
    {
        this.config = config;
        this.repository = repository;
        this.context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult CreateToken([FromBody] LoginModel loginModel)
    {
        var user = Authenticate(loginModel);
        if (user is null)
            return Unauthorized();
        var tokenString = BuildToken(user);

        return Ok(new { token = tokenString });
    }

    private string BuildToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new(JwtRegisteredClaimNames.Email, user.Login),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Acr, user.Name)
        };
        foreach (var role in user.Roles)
            claims.Add(new Claim("role", role.ToString()));

        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel Authenticate(LoginModel loginModel)
    {
        // var userDal = repository.GetUser(loginModel);
        var userDal = new UserDal { Id = 2, Login = loginModel.Login, Password = loginModel.Password, Name = "Random Name" };
        userDal = context.Users.First(u => u.Name == "masha");
        if (userDal is null) return null;

        var user = new UserModel(userDal);
        var user2 = new UserModel(userDal);
        var res = user.PasswordHasher.VerifyHashedPassword(user,user.Password, "gfdgd");
        
        // var user = new UserModel { Name = userDal.Id.ToString(), Email = userDal.Email };

        return user;
    }
}