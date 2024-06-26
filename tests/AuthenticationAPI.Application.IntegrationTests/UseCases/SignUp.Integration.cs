using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Application.Common.DTOs;
using AuthenticationAPI.Application.Common.Interfaces;
using AuthenticationAPI.Application.UseCases;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;
using AuthenticationAPI.Infrastructure.Persistence;
using AuthenticationAPI.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using AuthenticationAPI.Application.Common.Interfaces.Services;
using AuthenticationAPI.Application.Services;
using AuthenticationAPI.Infrastructure.Common.Interfaces;
using AuthenticationAPI.Application.Adapters;

namespace AuthenticationAPI.Application.IntegrationTests.UseCases;


[TestFixture()]
public class SignUpIntegration
{
    private readonly DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("in-memory")
        .Options;

    [Test]
    public void Should_Throw_When_EmailIsWrong()
    {
        SignUpDTO signUpDTO = new() {
            Email = "wrongemail...",
            Password = "thestrongestpassword"
        };

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);

        SignUp signUp = new SignUp(accountRepository);

        Exception? ex = Assert.Throws<Exception>(() => signUp.Execute(signUpDTO));
        Assert.That(ex?.Message, Is.EqualTo("Email is not valid."));
    }

    [Test]
    public void Should_Throw_When_EmailAlreadyExists()
    {
        Email email = Email.Create("itsabeautiful@email.com");
        string password = "thestrongestpassword";
        SignUpDTO signUpDTO = new() {
            Email = email,
            Password = password
        };

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Account.Add(new Account(email, password));
        context.SaveChanges();

        IAccountRepository accountRepository = new AccountRepository(context);
        SignUp signUp = new SignUp(accountRepository);

        Exception? ex = Assert.Throws<Exception>(() => signUp.Execute(signUpDTO));
        Assert.That(ex?.Message, Is.EqualTo("Email has already been in use."));
    }

    [Test]
    public void Should_CreateAccount_When_PayloadIsValid()
    {
        Email email = Email.Create("itsabeautiful@email.com");
        string password = "thestrongestpassword";
        SignUpDTO signUpDTO = new() {
            Email = email,
            Password = password
        };

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);
        SignUp signUp = new SignUp(accountRepository);

        Account account = signUp.Execute(signUpDTO);

        Assert.That(account, Is.TypeOf<Account>());
        Assert.Multiple((TestDelegate)(() =>
        {
            Assert.That((string)account.Email.Value, Is.EqualTo(email.Value));
            Assert.That(account.Password, Is.Not.EqualTo(password));
            Assert.That(account.Id, Is.Not.Null);
        }));
    }
}

