using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Application.Common.Interfaces;
using AuthenticationAPI.Application.UseCases;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;
using AuthenticationAPI.Infrastructure.Persistence;
using AuthenticationAPI.Infrastructure.Repositories;

namespace AuthenticationAPI.Application.IntegrationTests.UseCases;

public class SignInIntegration
{
    [Test]
    public void Should_Throw_When_EmailIsWrong()
    {
        string email = "wrongemail...";
        string password = "thestrongestpassword";

        var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("in-memory")
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);
        SignIn signIn = new SignIn(accountRepository);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Email is not valid."));
    }

    [Test]
    public void Should_Throw_When_AccountNotFound()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("in-memory")
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);
        SignIn signIn = new SignIn(accountRepository);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Account does not exist."));
    }

    [Test]
    public void Should_Throw_When_PasswordIsInvalid()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("in-memory")
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword("different-password");

        context.Account.Add(new Account(Email.Create(email), hashedPassword));

        IAccountRepository accountRepository = new AccountRepository(context);
        SignIn signIn = new SignIn(accountRepository);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Account does not exist."));
    }

    [Test]
    public void Should_Login_When_LoginInfoIsCorrect()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        var _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("in-memory")
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        context.Account.Add(new Account(Email.Create(email), hashedPassword));
        context.SaveChanges();

        IAccountRepository accountRepository = new AccountRepository(context);
        SignIn signIn = new SignIn(accountRepository);

        bool isValid = signIn.Execute(email, password);

        Assert.That(isValid, Is.True);
    }
}

