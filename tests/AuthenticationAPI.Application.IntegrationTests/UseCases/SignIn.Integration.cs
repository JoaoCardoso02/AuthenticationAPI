using Microsoft.EntityFrameworkCore;
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
public class SignInIntegration
{

    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string> {
                { "AccessTokenSecurityKey", "MyUltraMegaBlasterAccessTokenSecurityKey" },
        }).Build();

    private readonly DbContextOptions<ApplicationDbContext> _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("in-memory")
        .Options;

    [Test]
    public void Should_Throw_When_EmailIsWrong()
    {
        string email = "wrongemail...";
        string password = "thestrongestpassword";

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);
        ISecurityService securityService = new SecurityService(_configuration);

        SignIn signIn = new SignIn(accountRepository, securityService);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Email is not valid."));
    }

    [Test]
    public void Should_Throw_When_AccountNotFound()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        IAccountRepository accountRepository = new AccountRepository(context);
        ISecurityService securityService = new SecurityService(_configuration);
        SignIn signIn = new SignIn(accountRepository, securityService);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Account does not exist."));
    }

    [Test]
    public void Should_Throw_When_PasswordIsInvalid()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword("different-password");

        context.Account.Add(new Account(Email.Create(email), hashedPassword));

        IAccountRepository accountRepository = new AccountRepository(context);
        ISecurityService securityService = new SecurityService(_configuration);
        SignIn signIn = new SignIn(accountRepository, securityService);

        Exception? ex = Assert.Throws<Exception>(() => signIn.Execute(email, password));
        Assert.That(ex?.Message, Is.EqualTo("Account does not exist."));
    }

    [Test]
    public void Should_Login_When_LoginInfoIsCorrect()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        string hashedPassword = CryptographyAdapter.HashPassword(password);

        context.Account.Add(new Account(Email.Create(email), hashedPassword));
        context.SaveChanges();

        IAccountRepository accountRepository = new AccountRepository(context);
        ISecurityService securityService = new SecurityService(_configuration);
        SignIn signIn = new SignIn(accountRepository, securityService);

        string token = signIn.Execute(email, password);

        Assert.That(token, Is.Not.Empty);
    }

    [Test]
    public void Should_ValidateToken_When_LoginIsSucessfull()
    {
        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        using ApplicationDbContext context = new ApplicationDbContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        string hashedPassword = CryptographyAdapter.HashPassword(password);

        context.Account.Add(new Account(Email.Create(email), hashedPassword));
        context.SaveChanges();

        IAccountRepository accountRepository = new AccountRepository(context);
        ISecurityService securityService = new SecurityService(_configuration);
        SignIn signIn = new SignIn(accountRepository, securityService);

        string token = signIn.Execute(email, password);

        AuthPayload payload = securityService.GetPayload(token);

        Assert.That(payload.Id, Is.TypeOf<int>());
        Assert.That(payload.Email, Is.TypeOf<string>());
        Assert.That(payload.Email, Is.EqualTo(email));
    }
}

