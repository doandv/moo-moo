using Microsoft.AspNetCore.Mvc;
using MooMoo.Application.Auth.DTOs;
using MooMoo.Application.Auth.UseCases;

namespace MooMoo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterWithEmailUseCase _registerWithEmailUseCase;
    private readonly LoginWithEmailUseCase _loginWithEmailUseCase;

    public AuthController(
        RegisterWithEmailUseCase registerWithEmailUseCase,
        LoginWithEmailUseCase loginWithEmailUseCase)
    {
        _registerWithEmailUseCase = registerWithEmailUseCase;
        _loginWithEmailUseCase = loginWithEmailUseCase;
    }

    /// <summary>
    /// POST /api/auth/register-with-email
    /// US01: Register new parent account with email/password
    /// </summary>
    [HttpPost("register-with-email")]
    public async Task<ActionResult<RegisterWithEmailResponse>> RegisterWithEmail([FromBody] RegisterWithEmailRequest request)
    {
        var response = await _registerWithEmailUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(RegisterWithEmail), response);
    }

    /// <summary>
    /// POST /api/auth/login
    /// US04: Login with email/password
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginWithEmailResponse>> Login([FromBody] LoginWithEmailRequest request)
    {
        var response = await _loginWithEmailUseCase.ExecuteAsync(request);
        return Ok(response);
    }
}
