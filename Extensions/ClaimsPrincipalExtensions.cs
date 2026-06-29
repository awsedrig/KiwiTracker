using System.Security.Claims;

namespace KiwiTracker.API.Extensions;

/// <summary>
/// Extension methods for extracting user information from JWT claims.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Extracts the authenticated user's ID from the JWT NameIdentifier claim.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">Thrown when the claim is missing or invalid.</exception>
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out var userId))
        {
            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
        }

        return userId;
    }
}
