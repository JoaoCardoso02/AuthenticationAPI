using AuthenticationAPI.Application.Common.DTOs;
using AuthenticationAPI.Domain.Entities;

namespace AuthenticationAPI.Application.Common.Interfaces.UseCases;

public interface ISignUp
{
    Account Execute(SignUpDTO signUp);
}