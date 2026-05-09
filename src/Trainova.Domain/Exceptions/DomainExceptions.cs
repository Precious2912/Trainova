namespace Trainova.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

public class UserNotFoundException : DomainException
{
    public UserNotFoundException(string email) : base($"User with email {email} was not found.") { }

    public UserNotFoundException(Guid id) : base($"User with ID {id} was not found.") { }
}

public class InvalidCredentials : DomainException
{
    public InvalidCredentials() : base("Invalid email or password.") { }
}
