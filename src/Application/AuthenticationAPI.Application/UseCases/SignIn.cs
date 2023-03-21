using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Application.UseCases;

public class SignIn
{
    private IAccountRepository _accountRepository;

    public SignIn(
        IAccountRepository accountRepository
    ) {
        _accountRepository = accountRepository;
    }

    public Account Execute(string email, string password)
    {
        Email emailResult = Email.Create(email);

        Account account = new Account(emailResult, password);

        _accountRepository.GetAccount(emailResult);

        return account;
    }
}
