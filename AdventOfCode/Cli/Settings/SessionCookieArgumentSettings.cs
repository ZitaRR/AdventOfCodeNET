using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Settings;

public class SessionCookieArgumentSettings : CommandSettings
{
    [CommandArgument(0, "<sessionCookie>")]
    public string SessionCookie { get; set; } = string.Empty;
}
