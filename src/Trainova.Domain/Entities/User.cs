using Trainova.Domain.Enums;

namespace Trainova.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }

    private User() { }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        UserRole role = UserRole.Passenger)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email.ToLower().Trim(),
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };
    }

    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        ModifiedAt = DateTime.UtcNow;
    }
}
