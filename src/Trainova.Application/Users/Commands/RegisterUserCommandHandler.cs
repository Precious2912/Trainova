using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainova.Application.Common.Interfaces;
using Trainova.Application.Common.Models;
using Trainova.Domain.Entities;
using Trainova.Domain.Enums;

namespace Trainova.Application.Users.Commands;

public class RegisterUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly IAppDbContext _context = context;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email.ToLower().Trim(), cancellationToken);

        if (emailExists)
            return Result<string>.Fail("Email is already registered.");

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            UserRole.Passenger
        );

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<string>.Ok("Registration successful.");
    }
}
