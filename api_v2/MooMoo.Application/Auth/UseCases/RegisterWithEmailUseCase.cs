using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MooMoo.Application.Auth.DTOs;
using MooMoo.Application.Common.Interfaces;
using MooMoo.Application.Common.Constants;
using MooMoo.Application.Exceptions;
using MooMoo.Domain.Entities;
using MooMoo.Domain.Enums;

namespace MooMoo.Application.Auth.UseCases;

public class RegisterWithEmailUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RegisterWithEmailRequest> _validator;

    public RegisterWithEmailUseCase(
        IApplicationDbContext context,
        IValidator<RegisterWithEmailRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<RegisterWithEmailResponse> ExecuteAsync(RegisterWithEmailRequest request)
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // 1. Check if email already exists (any provider)
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            throw new ConflictException(MessageCodes.ERROR_EMAIL_ALREADY_EXISTS, "email");
        }

        // 2. Hash password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, 10);

        // 3. Create user account (PARENT role by default)
        var user = new User
        {
            Email = request.Email,
            PasswordHash = hashedPassword,
            Role = UserRole.PARENT,
            Provider = UserProvider.EMAIL,
            Status = UserStatus.ACTIVE,
            EmailVerified = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 4. Create default parent profile
        var profile = new Profile
        {
            Name = request.Name,
            ParentId = user.Id,
            Grass = 0,
            Gold = 0
        };

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        return new RegisterWithEmailResponse
        {
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            },
            Message = "Account created successfully. You can now login."
        };
    }
}
