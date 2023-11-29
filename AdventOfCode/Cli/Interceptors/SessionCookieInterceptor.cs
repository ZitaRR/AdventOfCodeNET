using AdventOfCodeNet.Cli.Providers;
using AdventOfCodeNet.Cli.Settings;
using AdventOfCodeNet.Core.IO;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Interceptors;

public sealed class SessionCookieInterceptor : ICommandInterceptor
{
    private readonly IFileManager fileManager;

    public SessionCookieInterceptor(IFileManager fileManager)
    {
        this.fileManager = fileManager;
    }

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is not AdventOfCodeSettings aocSettings)
        {
            return;
        }

        SetSessionCookie(aocSettings);
    }

    private void SetSessionCookie(AdventOfCodeSettings settings)
    {
        if (!string.IsNullOrEmpty(settings.SessionCookie))
        {
            SessionCookieProvider.SessionCookie = settings.SessionCookie;
            return;
        }

        try
        {
            SessionCookieProvider.SessionCookie = fileManager
                .GetSessionCookieAsync()
                .GetAwaiter()
                .GetResult();
        }
        catch { }
    }
}
