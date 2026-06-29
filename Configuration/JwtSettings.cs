namespace KiwiTracker.API.Configuration;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = "KiwiTrackerAPI";
    public string Audience { get; init; } = "KiwiTrackerClient";
    public int ExpirationInDays { get; init; } = 7;
}
