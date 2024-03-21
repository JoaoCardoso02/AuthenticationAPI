using AuthenticationAPI.Application.Common.DTOs;

namespace AuthenticationAPI.Application.Common.Interfaces.UseCases;

public interface ISignIn
{
    string Execute(SignInDTO signIn);
}

