namespace KiwiTracker.API.Exceptions;

/// <summary>
/// Base exception for domain-specific errors.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

/// <summary>
/// Thrown when a requested resource does not exist.
/// Maps to HTTP 404.
/// </summary>
public sealed class NotFoundException : DomainException
{
    public NotFoundException(string resourceName, object id)
        : base($"{resourceName} with identifier '{id}' was not found.") { }
}

/// <summary>
/// Thrown when an operation conflicts with the current state (e.g., duplicate email).
/// Maps to HTTP 409.
/// </summary>
public sealed class ConflictException : DomainException
{
    public ConflictException(string message) : base(message) { }
}

/// <summary>
/// Thrown when authentication fails (invalid credentials).
/// Maps to HTTP 401.
/// </summary>
public sealed class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message = "Invalid credentials.") : base(message) { }
}

/// <summary>
/// Thrown when the user does not have permission to access a resource.
/// Maps to HTTP 403.
/// </summary>
public sealed class ForbiddenException : DomainException
{
    public ForbiddenException(string message = "You do not have access to this resource.") : base(message) { }
}
