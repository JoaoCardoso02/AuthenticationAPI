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

    public bool Execute(string email, string password)
    {
        Email emailResult = Email.Create(email);

        Account? account = _accountRepository.GetAccount(emailResult);

        if (account == null)
            throw new Exception("Account does not exist.");

        bool samePassword = BCrypt.Net.BCrypt.Verify(password, account.Password);

        if (samePassword == false)
            throw new Exception("Invalid password.");

        return true;
    }
}
