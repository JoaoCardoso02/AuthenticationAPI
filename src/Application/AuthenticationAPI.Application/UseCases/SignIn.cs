using AuthenticationAPI.Application.Adapters;
using AuthenticationAPI.Application.Common.Interfaces.Services;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Application.UseCases;

public class SignIn
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISecurityService _securityService;

    public SignIn(
        IAccountRepository accountRepository,
        ISecurityService securityService
    ) {
        _accountRepository = accountRepository;
        _securityService = securityService;
    }

    public string Execute(string email, string password)
    {
        Email emailResult = Email.Create(email);

        Account? account = _accountRepository.GetAccount(emailResult);

        if (account == null || account.Id == null)
            throw new Exception("Account does not exist.");

        CryptographyAdapter.Verify(password, account.Password);

        return _securityService.GenerateAccessToken(account);
    }
}
