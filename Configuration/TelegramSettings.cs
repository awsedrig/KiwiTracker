namespace KiwiTracker.API.Configuration;

public sealed class TelegramSettings
{
    public const string SectionName = "TelegramBot";

    public string Token { get; init; } = string.Empty;
}
