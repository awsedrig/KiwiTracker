using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using KiwiTracker.API.Configuration;

namespace KiwiTracker.Bot;

public class TelegramBotWorker : BackgroundService
{
    private readonly ILogger<TelegramBotWorker> _logger;
    private readonly ITelegramBotClient _botClient;

    public TelegramBotWorker(ILogger<TelegramBotWorker> logger, IOptions<TelegramSettings> settings)
    {
        _logger = logger;

        var token = settings.Value.Token;
        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogWarning("Telegram bot token is not configured. Bot will not start.");
            _botClient = null!;
            return;
        }

        _botClient = new TelegramBotClient(token);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_botClient == null)
        {
            _logger.LogWarning("Telegram bot is disabled due to missing token.");
            return;
        }

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions { AllowedUpdates = [] },
            cancellationToken: stoppingToken
        );

        await _botClient.SetMyCommands(new[]
        {
            new BotCommand { Command = "/start", Description = "Start the bot" },
            new BotCommand { Command = "/ping", Description = "Check connection" },
        }, cancellationToken: stoppingToken);

        var me = await _botClient.GetMe(cancellationToken: stoppingToken);
        _logger.LogInformation("Telegram bot @{BotUsername} started successfully", me.Username);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText } message)
            return;

        var chatId = message.Chat.Id;
        var text = messageText.Trim().ToLower();

        _logger.LogInformation("Received command from chat {ChatId}: {Command}", chatId, text);

        var response = text switch
        {
            "/start" => "Welcome to KiwiTracker Bot!\n\nAvailable commands:\n/ping — check connection",
            "/ping" => "Pong! Server is running.",
            _ => $"Unknown command: {text}. Use /start to see available commands."
        };

        await botClient.SendMessage(chatId: chatId, text: response, cancellationToken: cancellationToken);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Telegram API polling error");
        return Task.CompletedTask;
    }
}