using KiwiTracker.API.DTOs;

namespace KiwiTracker.API.Services;

/// <summary>
/// Handles user authentication: registration, login, and token refresh.
/// </summary>
public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
