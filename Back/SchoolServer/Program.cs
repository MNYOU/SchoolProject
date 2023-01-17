using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolServer.Data;
using SchoolServer.Data.Entities;
using SchoolServer.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// services.AddIdentityCore<AppUser>();
// var identityBuilder = new IdentityBuilder(builder, builder.Services);
// services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options => options.LoginPath = "/account/login");
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            // // указывает, будет ли валидироваться издатель при валидации токена
            // ValidateIssuer = true,
            // // строка, представляющая издателя
            // ValidIssuer = AuthOptions.ISSUER,
            // // будет ли валидироваться потребитель токена
            // ValidateAudience = true,
            // // установка потребителя токена
            // ValidAudience = AuthOptions.AUDIENCE,
            // // будет ли валидироваться время существования
            // ValidateLifetime = true,
            // // установка ключа безопасности
            // IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // // валидация ключа безопасности
            // ValidateIssuerSigningKey = true,
        };
    });
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});
// services.AddIdentity<IdentityUser, IdentityRole>();
services.AddAuthorization();
services.AddRouting();
services.AddTransient<IDbRepository, DbRepository>(); //так лучше, но запарнее. оставим на будущее
services.AddTransient<DbRepository>();
services.AddSingleton<Random>();
// services.AddDbContext<DataContext>(options =>
// {
//     options.UseNpgsql(
//         builder.Configuration.GetConnectionString("DataAccessPostgreSqlProvider")); // название вытягивается из json'a
// });
services.AddDbContext<DataContext>(options => { });
services.AddTransient<IEntityDal, DifficultyDal>();


var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // нужно для отладки
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.MapControllers();
// app.Map("", async context =>
// {
//     var user = context.User;
//     context.Response.Redirect("home.html");
// });

app.Map("account/login",
    async context => { await context.Response.WriteAsync("this page shows that you are not logged in"); });
app.Run();