using AuthenticationAPI.Domain.Entities;

namespace AuthenticationAPI.Application.Common.Interfaces.Services;

public interface ISecurityService
{
    string GenerateAccessToken(Account account);
    AuthPayload GetPayload(string token);
}

