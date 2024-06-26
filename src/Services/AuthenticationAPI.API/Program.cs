using AuthenticationAPI.Application.Common.Interfaces.Services;
using AuthenticationAPI.Application.Common.Interfaces.UseCases;
using AuthenticationAPI.Application.Common.Interfaces;
using AuthenticationAPI.Application.Services;
using AuthenticationAPI.Application.UseCases;
using AuthenticationAPI.Infrastructure.Common.Interfaces;
using AuthenticationAPI.Infrastructure.Persistence;
using AuthenticationAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<ISignIn, SignIn>();
builder.Services.AddScoped<ISignUp, SignUp>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddSingleton<ISecurityService, SecurityService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
