using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolServer.Data;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;
using SchoolServer.Models;

namespace SchoolServer.Controllers;

[ApiController]
public class TokenController : Controller
{
    // в теории пользователи могут создать несколько аккаунтов под одним логином
    private readonly IConfiguration config;
    private readonly DataContext context;
    private readonly DbRepository repository;

    public TokenController(IConfiguration config, DbRepository repository, DataContext context)
    {
        this.config = config;
        this.repository = repository;
        this.context = context;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register/")]
    public async Task<IActionResult> Register([FromBody] LoginModel loginModel)
    {
        var userDal = new UserDal() {Name = "defaultName", Login = loginModel.Login, Password = loginModel.Password};
        var hasher = new PasswordHasher<UserDal>();
        var hashed = hasher.HashPassword(userDal, loginModel.Password);
        userDal = new UserDal() {Name = "defaultName", Login = loginModel.Login, Password = hashed};
        context.Users.Add(userDal);
        await context.SaveChangesAsync();
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login/")]
    public IActionResult CreateToken([FromBody] LoginModel loginModel)
    {
        var user = Authenticate(loginModel);
        if (user is null)
            return Unauthorized();
        var tokenString = BuildToken(user);
        return Json(new { token = tokenString});
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
        claims.AddRange(user.Roles.Select(role => new Claim("role", role.ToString())));
        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel? Authenticate(LoginModel loginModel)
    {
        var user = context.Users.FirstOrDefault(u => u.Login == loginModel.Login);
        if (user is null)
        {
            return null;
        }
        var hasher = new PasswordHasher<UserDal>();
        var verifyResult = hasher.VerifyHashedPassword(user, user.Password, loginModel.Password);
        return verifyResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded ? new UserModel(user) : null;
    }
}