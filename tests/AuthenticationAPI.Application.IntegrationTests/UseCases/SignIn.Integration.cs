using System;
using AuthenticationAPI.Application.Common.Interfaces;
using AuthenticationAPI.Application.UseCases;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Infrastructure.Common.Interfaces;
using AuthenticationAPI.Infrastructure.Persistence;
using AuthenticationAPI.Infrastructure.Repositories;
using NUnit;
namespace Application.IntegrationTests.UseCases;

public class SignInIntegration {
    [Test]
    public void ShouldSignInSuccessfully() {
        IApplicationDbContext dbContext = new ApplicationDbContext();
        IAccountRepository accountRepository = new AccountRepository(dbContext);
        SignIn signIn = new SignIn(accountRepository);

        string email = "itsabeautiful@email.com";
        string password = "thestrongestpassword";

        Account account = signIn.Execute(email, password);

        Assert.NotNull(account);
    }
}

