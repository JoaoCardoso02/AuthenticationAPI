using System;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Application.Common.Interfaces;

public interface IAccountRepository
{
    Account? GetAccount(Email email);
    int CreateAccount(Account account);
}
