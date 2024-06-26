using AuthenticationAPI.Application.Common.Interfaces.UseCases;
using AuthenticationAPI.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using AuthenticationAPI.Domain.Entities;

namespace AuthenticationAPI.API.Controllers;

[ApiController]
public class AuthController : Controller
{
    private readonly ISignIn _signIn;
    private readonly ISignUp _signUp;

    public AuthController(
        ISignIn signIn,
        ISignUp signUp
    )
    {
        _signIn = signIn;
        _signUp = signUp;
    }

    [HttpPost("signin")]
    public IActionResult SignIn([FromBody] SignInDTO signIn)
    {
        string token = _signIn.Execute(signIn);
        return Ok(token);
    }

    [HttpPost("signup")]
    public IActionResult SignUp([FromBody] SignUpDTO signUp)
    {
        Account account = _signUp.Execute(signUp);
        return Ok(account);
    }
}

