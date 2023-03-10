using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Application.UseCases;

public class SignIn
{
    public Account Execute(string email, string password)
    {
        Email emailResult = Email.Create(email);

        Account account = new Account { Email = emailResult, Password = password };

        return account;
    }
}
