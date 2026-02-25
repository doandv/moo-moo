using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MooMoo.Application.Auth.DTOs;
using MooMoo.Application.Common.Constants;
using MooMoo.Application.Common.Interfaces;
using MooMoo.Application.Exceptions;
using MooMoo.Domain.Enums;

namespace MooMoo.Application.Auth.UseCases;

public class LoginWithEmailUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<LoginWithEmailRequest> _validator;
    private readonly IJwtService _jwtService;

    public LoginWithEmailUseCase(
        IApplicationDbContext context,
        IValidator<LoginWithEmailRequest> validator,
        IJwtService jwtService)
    {
        _context = context;
        _validator = validator;
        _jwtService = jwtService;
    }

    public async Task<LoginWithEmailResponse> ExecuteAsync(LoginWithEmailRequest request)
    {
        // 1. Validate request
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // 2. Find user by email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.Provider == UserProvider.EMAIL);

        // 3. Check if user exists and password is correct
        if (user == null || string.IsNullOrEmpty(user.PasswordHash) || 
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            // Don't reveal which field is wrong (security best practice)
            throw new UnauthorizedException(MessageCodes.ERROR_INVALID_CREDENTIALS);
        }

        // 4. Check account status
        if (user.Status == UserStatus.BLOCKED)
        {
            throw new ForbiddenException(MessageCodes.ERROR_ACCOUNT_LOCKED);
        }

        // 5. Update last login timestamp
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // 6. Generate JWT token
        var accessToken = _jwtService.GenerateAccessToken(user);

        return new LoginWithEmailResponse
        {
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            },
            AccessToken = accessToken,
            Message = MessageCodes.SUCCESS_LOGIN_SUCCESS
        };
    }
}
