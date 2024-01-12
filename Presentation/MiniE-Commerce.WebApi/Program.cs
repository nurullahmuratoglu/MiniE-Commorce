using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiniE_Commerce.Infrastructure;
using MiniE_Commerce.Persistence;
using MiniE_Commerce.Persistence.Repositories.Category;
using MiniE_Commerce.Persistence.Repositories.Product;
using MiniE_Commorce.Application;
using MiniE_Commorce.Application.Interfaces.Repositories.Category;
using MiniE_Commorce.Application.Interfaces.Repositories.Product;
using Serilog;
using Serilog.Core;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir. -> www.bilmemne.com
            ValidateIssuer = true, //Oluşturulacak token değerini kimin dağıttını ifade edeceğimiz alandır. -> www.myapi.com
            ValidateLifetime = true, //Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
            ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulanmasıdır.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.

        };
    });


Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    



    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAplication();
builder.Services.AddInfrastructure();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
