using AdventOfCodeNet.Cli.Providers;
using AdventOfCodeNet.Cli.Settings;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Commands;

public abstract class SessionCookieCommandBase<T> : AsyncCommand<T> where T : SessionCookieOptionSettings
{
    protected bool Validate(T settings)
    {
        return
            !string.IsNullOrEmpty(settings.SessionCookie) ||
            !string.IsNullOrEmpty(SessionCookieProvider.SessionCookie);
    }
}
