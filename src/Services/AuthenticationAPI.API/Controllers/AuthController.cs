using AuthenticationAPI.Application.Common.Interfaces.UseCases;
using AuthenticationAPI.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.API.Controllers;

[ApiController]
public class AuthController : Controller
{
    private readonly ISignIn _signIn;

    public AuthController(ISignIn signIn)
    {
        _signIn = signIn;
    }

    [HttpPost("signin")]
    public IActionResult SignIn([FromBody] SignInDTO signIn)
    {
        string token = _signIn.Execute(signIn);
        return Ok(token);
    }
}

