using MediatR;
using Trainova.Application.Common.Models;

namespace Trainova.Application.Users.Commands;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
) : IRequest<Result<string>>;