using AuthenticationAPI.Application.Adapters;
using AuthenticationAPI.Application.Common.DTOs;
using AuthenticationAPI.Application.Common.Interfaces.Services;
using AuthenticationAPI.Application.Common.Interfaces.UseCases;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Application.UseCases;

public class SignUp : ISignUp
{
    private readonly IAccountRepository _accountRepository;

    public SignUp(
        IAccountRepository accountRepository
    ) {
        _accountRepository = accountRepository;
    }

    public Account Execute(SignUpDTO signUp)
    {
        Email email = Email.Create(signUp.Email);

        CheckIfEmailIsAlreadyInUse(email);

        string password = CryptographyAdapter.HashPassword(signUp.Password);
        Account account = new(email, password);

        account = _accountRepository.CreateAccount(account);

        if (account == null || account.Id == null)
            throw new Exception("Failed to created account.");

        return account;
    }

    void CheckIfEmailIsAlreadyInUse(Email email)
    {
        bool isEmailAlreadyRegistered = _accountRepository.GetAccount(email) != null;

        if (isEmailAlreadyRegistered)
            throw new Exception("Email has already been in use.");
    }
}